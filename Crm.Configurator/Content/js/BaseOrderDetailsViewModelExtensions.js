$(function () {

  var baseOrderDetailsViewModel = namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel;
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel = function () {
    var viewModel = this;
    baseOrderDetailsViewModel.apply(this, arguments);
    viewModel.articleGroups = window.ko.observableArray([]);
    viewModel.articleGroups01 = window.ko.observableArray([]);
    viewModel.articleGroups02 = window.ko.observableArray([]);
    viewModel.articleGroups03 = window.ko.observableArray([]);
    viewModel.articleGroups04 = window.ko.observableArray([]);
    viewModel.articleGroups05 = window.ko.observableArray([]);
    viewModel.configurationBase = window.ko.observable(null);
    viewModel.configurationRules = window.ko.observableArray([]);
    viewModel.defaultVariableValues = window.ko.observableArray([]);
    viewModel.defaultVariableValueArticleIds = window.ko.pureComputed(function () {
      return viewModel.defaultVariableValues().map(function (x) { return window.ko.unwrap(x.ChildId); });
    });
    viewModel.requiredVariableValues = window.ko.observableArray([]);
    viewModel.requiredVariableValueArticleIds = window.ko.pureComputed(function () {
      return viewModel.requiredVariableValues().map(function (x) { return window.ko.unwrap(x.ChildId); });
    });
    // TODO: move to configurationtab viewmodel
    viewModel.variables = window.ko.observableArray([]);
    viewModel.variables.distinct("ArticleGroup01Key");
    viewModel.variables.distinct("ArticleGroup02Key");
    viewModel.variables.distinct("ArticleGroup03Key");
    viewModel.variables.distinct("ArticleGroup04Key");
    viewModel.variables.distinct("ArticleGroup05Key");
    // variable value selection
    // TODO: move to configurationtab viewmodel ?
    viewModel.selectedVariableValues = window.ko.observableArray([]);
    viewModel.selectedOptionalVariableValues = window.ko.observableArray([]);
    viewModel.variableValues = window.ko.observableArray([]);
    viewModel.variableValueFilter = window.ko.observable("");
    viewModel.variableValueFilterByIsDefault = window.ko.observable(null);
    viewModel.variableValueVariableId = window.ko.observable(null);
    viewModel.selectedVariableValues.subscribe(function (selectedVariableValues) {
      viewModel.selectedOptionalVariableValues.remove(function (x) { return selectedVariableValues.indexOf(x) === -1; });
    });
    viewModel.variableValueSelectionPager = window.ko.custom.paging();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype = baseOrderDetailsViewModel.prototype;

  var baseInit = namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.init;
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.init = function(id, params) {
		var viewModel = this;
		if (!!params && !!params.configurationBaseId) {
			params.orderEntryType = "Configuration";
		}
		return baseInit.apply(viewModel, arguments).then(function() {
			return viewModel.loadConfigurationBase(id, params);
		}).then(function() {
			return viewModel.loadOrderItems(id, params);
		}).then(function() {
			return viewModel.loadArticleGroups(id, params);
		}).then(function() {
			return viewModel.loadRequiredAndDefaultVariableValues(id, params);
		});
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.loadConfigurationBase = function (id, params) {
		var viewModel = this;
		return new $.Deferred().resolve().promise()
			.pipe(function () {

				if (viewModel.baseOrder().OrderEntryType() === "Configuration" &&
					(!viewModel.baseOrder().ExtensionValues().ConfigurationBaseId() && !params.configurationBaseId)) {
					params.id = params.id || id || viewModel.baseOrder().Id();
					params.redirectPlugin = viewModel.plugin;
					params.redirectController = viewModel.controller;
					params.redirectAction = location.hash.indexOf("DetailsTemplate") !== -1
						? viewModel.action + "Template"
						: viewModel.action;
					var paramsString = Object.getOwnPropertyNames(params || {}).map(function (param) {
						return param + "=" + params[param];
					}).join("&");
					window.database.stateManager.trackedEntities.splice(0,
						window.database.stateManager.trackedEntities.length);
					viewModel.dispose();
					window.location.hash = "/Crm.Configurator/Configuration/IndexTemplate?" + paramsString;
					return new $.Deferred().promise();
				}

				if (!!params.configurationBaseId) {
					viewModel.baseOrder().ExtensionValues().ConfigurationBaseId(params.configurationBaseId);
				}
				var configurationBaseId = viewModel.baseOrder().ExtensionValues().ConfigurationBaseId();
				if (!configurationBaseId) {
					return;
				}
				return window.database.CrmConfigurator_ConfigurationBase
					.include("Variables")
					.include("ConfigurationRules")
					.filter(function (x) { return x.Id == this.Id; }, { Id: configurationBaseId })
					.toArray(function (results) {
						var configurationBase = results[0];
						viewModel.configurationBase(configurationBase.asKoObservable());
						var sortedVariables = configurationBase.Variables.sort(function (a, b) {
							return a.Description.localeCompare(b.Description);
						});
						viewModel.variables(window.ko.utils.arrayMap(sortedVariables,
							function (x) { return x.asKoObservable(); }));
					});
			}).pipe(function () {
				if (!viewModel.configurationBase()) {
					return;
				}
				return window.database.CrmConfigurator_ConfigurationRule
					.filter(function (x) { return x.ConfigurationBaseId == this.Id; },
						{ Id: viewModel.configurationBase().Id() })
					.toArray(function (results) {
						viewModel.configurationRules(results);
					});
			});
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.loadRequiredAndDefaultVariableValues = function (id, params) {
		var viewModel = this;
		return new $.Deferred().resolve().promise()
			.pipe(function () {
				return viewModel.getDefaultVariableValues()
					.then(function (defaultVariableValues) {
						viewModel.defaultVariableValues(defaultVariableValues);
					});
			})
			.pipe(function () {
				var variables = viewModel.configurationBase() === null ? [] : viewModel.configurationBase().Variables();
				var variableIds = variables.map(function (x) { return x.Id(); });
				return window.database.CrmArticle_ArticleRelationship
					.include("Child")
					.filter(function (x) {
							return x.RelationshipTypeKey == "VariableValue" &&
								x.ParentId in this.variableIds &&
								x.ExtensionValues.IsRequired == true;
						},
						{ variableIds: variableIds })
					.toArray(viewModel.requiredVariableValues);
			});
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.loadOrderItems = function (id, params) {
		var viewModel = this;
		return viewModel.loadUserAndVariableValues(id, params).then(function () {
				if (!id || viewModel.orderItems().length === 0) {
					if (!!viewModel.configurationBase() && !!viewModel.configurationBase().Price()) {
						viewModel.baseOrder().Price(viewModel.configurationBase().Price());
					}
					return viewModel.initDefaultOrderItems();
				}
				return null;
			})
			;
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.loadArticleGroups = function (id, params) {
		var viewModel = this;
		function loadArticleGroups(level) {
			var articleGroupKeys = viewModel.variables.indexKeys["ArticleGroup0" + level + "Key"]();
			if (articleGroupKeys.length === 0) {
				return;
			}

			return window.database["CrmArticle_ArticleGroup0" + level]
				.filter(function (x) { return x.Language == this.languageKey && x.Key in this.articleGroupKeys },
					{ languageKey: viewModel.user.DefaultLanguageKey, articleGroupKeys: articleGroupKeys })
				.toArray(function (results) {
					viewModel["articleGroups0" + level](window.ko.utils.arrayMap(results, function (x) { return x.asKoObservable(); }));
				});
		};
		return new $.Deferred().resolve().promise()
			.then(function () {
				return loadArticleGroups(1);
			})
			.pipe(function () {
				return loadArticleGroups(2);
			})
			.pipe(function () {
				return loadArticleGroups(3);
			})
			.pipe(function () {
				return loadArticleGroups(4);
			})
			.pipe(function () {
				return loadArticleGroups(5);
			})
			.pipe(function () {
				var id = 1;
				var maxLevel = 5;
				var variableArticleGroups = viewModel.variables().map(function (variable) {
					return {
						articleGroup01: variable.ArticleGroup01Key() === null
							? null
							: window.ko.utils.arrayFirst(viewModel.articleGroups01(),
								function (articleGroup) {
									return articleGroup.Key() === variable.ArticleGroup01Key();
								}) || null,
						articleGroup02: variable.ArticleGroup02Key() === null
							? null
							: window.ko.utils.arrayFirst(viewModel.articleGroups02(),
								function (articleGroup) {
									return articleGroup.Key() === variable.ArticleGroup02Key();
								}) || null,
						articleGroup03: variable.ArticleGroup03Key() === null
							? null
							: window.ko.utils.arrayFirst(viewModel.articleGroups03(),
								function (articleGroup) {
									return articleGroup.Key() === variable.ArticleGroup03Key();
								}) || null,
						articleGroup04: variable.ArticleGroup04Key() === null
							? null
							: window.ko.utils.arrayFirst(viewModel.articleGroups04(),
								function (articleGroup) {
									return articleGroup.Key() === variable.ArticleGroup04Key();
								}) || null,
						articleGroup05: variable.ArticleGroup05Key() === null
							? null
							: window.ko.utils.arrayFirst(viewModel.articleGroups05(),
								function (articleGroup) {
									return articleGroup.Key() === variable.ArticleGroup05Key();
								}) || null,
						variable: variable
					};
				});

				function addOrUpdateArticleGroupViewModel(parentViewModel, variableArticleGroup, level) {
					var articleGroupViewModel = window.ko.utils.arrayFirst(parentViewModel,
						function (x) {
							return x.articleGroup === variableArticleGroup["articleGroup0" + level];
						});
					if (!articleGroupViewModel) {
						articleGroupViewModel = {
							articleGroup: variableArticleGroup["articleGroup0" + level],
							childArticleGroups: [],
							id: id++,
							parent: parentViewModel,
							variables: [],
							visible: window.ko.observable(false)
						};
						parentViewModel.push(articleGroupViewModel);
						parentViewModel.sort(function (a, b) {
							if (a.articleGroup.SortOrder() !== b.articleGroup.SortOrder()) {
								return a.articleGroup.SortOrder() - b.articleGroup.SortOrder();
							}
							return a.articleGroup.Value().localeCompare(b.articleGroup.Value());
						});
					}
					if (level >= maxLevel || (level < maxLevel && variableArticleGroup["articleGroup0" + (level + 1)] === null)) {
						articleGroupViewModel.variables.push(variableArticleGroup.variable);
					} else {
						addOrUpdateArticleGroupViewModel(articleGroupViewModel.childArticleGroups, variableArticleGroup, level + 1);
					}
				};

				variableArticleGroups.forEach(function (variableArticleGroup) {
					addOrUpdateArticleGroupViewModel(viewModel.articleGroups(), variableArticleGroup, 1);
				});

				viewModel.articleGroups.valueHasMutated();
			});
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.loadUserAndVariableValues = function (id, params) {
		var viewModel = this;
		return new $.Deferred().resolve().promise()
			.pipe(function () {
				return window.Helper.User.getCurrentUser();
			})
			.pipe(function (user) {
				viewModel.user = user;
				if (!viewModel.baseOrder().ResponsibleUser()) {
					viewModel.baseOrder().ResponsibleUser(viewModel.user.Id);
				}
			})
			.then(function () {
				var variables = viewModel.configurationBase() === null ? [] : viewModel.configurationBase().Variables();
				var variableIds = variables.map(function (x) { return x.Id(); });
				return window.database.CrmArticle_ArticleRelationship
					.include("Child")
					.filter(function (x) {
						return x.RelationshipTypeKey == "VariableValue" && ((this.variableId != null && x.ParentId == this.variableId) || (this.variableId == null && x.ParentId in this.variableIds)) && (this.filter == "" || x.Child.ItemNo.startsWith(this.filter) == true || x.Child.Description.contains(this.filter) == true) && (this.isDefault == null || x.ExtensionValues.IsDefault == this.isDefault) && (this.alternativeSelectionArticleId == null || x.ChildId != this.alternativeSelectionArticleId);
					}, { variableId: viewModel.variableValueVariableId, variableIds: variableIds, filter: viewModel.variableValueFilter, isDefault: viewModel.variableValueFilterByIsDefault, alternativeSelectionArticleId: viewModel.alternativeSelectionArticleId });
			}).then(function (query) {
				return query
					.orderBy(function (x) { return x.Child.ItemNo; })
					.orderBy(function (x) { return x.Child.Description; })
					.skip(viewModel.variableValueSelectionPager.skip)
					.take(viewModel.variableValueSelectionPager.pageSize())
					.toArray(viewModel.variableValues)
					.then(function () { return query; });
			}).then(function (query) {
				return query
					.count(function (count) {
						viewModel.variableValueSelectionPager.totalItemCount(count);
					});
			}).pipe(function () {
				viewModel.variableValueFilter.subscribe(function () {
					viewModel.variableValueSelectionPager.page(1);
					viewModel.variableValueSelectionPager.totalItemCount(null);
				});
				viewModel.variableValueVariableId.subscribe(function () {
					viewModel.variableValues([]);
					viewModel.variableValueSelectionPager.page(1);
					viewModel.variableValueSelectionPager.totalItemCount(null);
				});
			});
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getArticleGroupPrefix = function (context) {
		var self = this;
		var prefix = context.$index() + 1 + ".";
		if (context.$parentContext && context.$parentContext.$index) {
			return self.getArticleGroupPrefix(context.$parentContext) + prefix;
		}
		return prefix;
	};
	namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getDefaultVariableValues = function() {
		var viewModel = this;
		var variables = viewModel.configurationBase() === null ? [] : viewModel.configurationBase().Variables();
		var variableIds = variables.map(function(x) { return x.Id(); });
		return window.database.CrmArticle_ArticleRelationship
			.include("Child")
			.filter(function(x) {
					return x.RelationshipTypeKey == "VariableValue" &&
						x.ParentId in this.variableIds &&
						x.ExtensionValues.IsDefault == true;
				},
				{ variableIds: variableIds })
			.toArray();
	};
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.initDefaultOrderItems = function () {
    var viewModel = this;
    var d = new $.Deferred();
    viewModel.defaultVariableValues().forEach(function (variableValue) {
        viewModel.addVariableValueAsOrderItem(variableValue.asKoObservable());
      });
      viewModel.orderItems.valueHasMutated();
    return d.resolve().promise();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.isDefaultOrderItem = function (orderItem) {
    return this.defaultVariableValueArticleIds().indexOf(orderItem.ArticleId()) !== -1;
  }
  // TODO: move to configuration tab viewmodel
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.toggleSelectedArticleGroup = function (toggledArticleGroupViewModel) {
    if ($(".collapsing:visible").length > 0) {
      return;
    }
    function hide(articleGroupViewModel) {
      if (articleGroupViewModel.visible() === true) {
        articleGroupViewModel.visible(false);
        articleGroupViewModel.childArticleGroups.forEach(function (childArticleGroup) {
          hide(childArticleGroup);
        });
      }
    }
    (toggledArticleGroupViewModel.parent || toggledArticleGroupViewModel).forEach(function (articleGroupViewModel) {
      if (articleGroupViewModel !== toggledArticleGroupViewModel) {
        hide(articleGroupViewModel);
      }
    });
    if (!window.ko.isWritableObservable(toggledArticleGroupViewModel.visible)) {
      return;
    }
    if (toggledArticleGroupViewModel.visible() === true) {
      hide(toggledArticleGroupViewModel);
    } else {
      toggledArticleGroupViewModel.visible(true);
    }
  };
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.applyVariableValues = function () {
    var viewModel = this;
    viewModel.loading(true);
    var variableValuesWithRules = window.ko.observableArray(viewModel.selectedVariableValues().slice());
    variableValuesWithRules.removeAll(viewModel.selectedOptionalVariableValues());
    var orderArticleIdsWithRules = window.ko.utils.arrayFilter(viewModel.orderItems(), function (x) {
      return x.IsAlternative() === false && x.IsOption() === false && x !== viewModel.alternativeSelectionFor();
    }).map(function (x) { return x.ArticleId(); });
    var selectedArticleIds = window.ko.utils.arrayFilter(viewModel.variableValues(), function (x) {
      return viewModel.selectedVariableValues().indexOf(x.ChildId()) !== -1 && viewModel.selectedOptionalVariableValues().indexOf(x.ChildId()) === -1 && orderArticleIdsWithRules.indexOf(x.ChildId()) === -1;
    }).map(function (x) { return x.ChildId(); });
    var deselectedOrderItemArticleIds = window.ko.utils.arrayFilter(viewModel.orderItems(), function (x) {
      if (viewModel.alternativeSelectionFor() === null) {
        return x.IsAccessory() === false && x.ParentOrderItemId() === null && (viewModel.variableValueVariableId() === null || viewModel.variableValueVariableId() === x.ExtensionValues().VariableId()) && viewModel.selectedVariableValues().indexOf(x.ArticleId()) === -1;
      } else {
        return x.ParentOrderItemId() === viewModel.alternativeSelectionFor().Id() && viewModel.selectedVariableValues().indexOf(x.ArticleId()) === -1;
      }
    }).map(function (x) { return x.ArticleId(); });
    var articleIds = window.ko.observableArray(window._.union(variableValuesWithRules(), orderArticleIdsWithRules));
    var articleIdsAfterRules = window.ko.observableArray(viewModel.applyRules(articleIds(), selectedArticleIds, false));
    articleIdsAfterRules.removeAll(deselectedOrderItemArticleIds);
    articleIdsAfterRules(viewModel.applyRules(articleIdsAfterRules(), selectedArticleIds, true));
    var currentOptionalOrderItemArticleIds = viewModel.getOptionalOrderItemsForVariable(null).map(function(orderItem) { return orderItem.ArticleId() });
    var articleIdsToAdd = window._.union(articleIdsAfterRules().filter(function (x) {
      return orderArticleIdsWithRules.indexOf(x) === -1;
    }), viewModel.selectedOptionalVariableValues().filter(function (x) {
      return currentOptionalOrderItemArticleIds.indexOf(x) === -1;
    }));
    var articleIdsToRemove = window._.union(orderArticleIdsWithRules.filter(function (x) {
      return articleIdsAfterRules().indexOf(x) === -1;
    }), deselectedOrderItemArticleIds);
    viewModel.confirmApplyRules(articleIdsToAdd, articleIdsToRemove).pipe(function () {
      return viewModel.addArticleIds(articleIdsToAdd);
    }).pipe(function () {
      return viewModel.removeArticleIds(articleIdsToRemove);
    }).pipe(function () {
      viewModel.orderItems.valueHasMutated();
      viewModel.loading(false);
      $("#variable-value-selection").modal("hide");
    }).fail(function () {
      viewModel.loading(false);
      window.swal(window.Helper.String.getTranslatedString("UnknownError"),
        window.Helper.String.getTranslatedString("Error_InternalServerError"),
        "error");
    });
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.addArticleIds = function (articleIds) {
    var viewModel = this;
    var d = new $.Deferred();
    var variableIds = viewModel.variables().map(function (x) { return x.Id(); });
  	window.database.CrmArticle_ArticleRelationship
		.include("Child")
		.filter(function (x) {
		  return x.ParentId in this.variableIds && x.RelationshipTypeKey === "VariableValue" && x.ChildId in this.articleIds;
		}, { articleIds: articleIds, variableIds: variableIds })
		.forEach(function (variableValue) {
		  viewModel.addVariableValueAsOrderItem(variableValue.asKoObservable());
		}).done(function () {
		  viewModel.orderItems.valueHasMutated();
		  d.resolve();
		}).fail(d.reject);
    return d.promise();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.removeArticleIds = function (articleIds) {
    var viewModel = this;
    var d = new $.Deferred();
    var orderItemsToRemove = viewModel.orderItems().filter(function (orderItem) {
      return articleIds.indexOf(orderItem.ArticleId()) !== -1;
    });
    var orderItemIdsToRemove = orderItemsToRemove.map(function (orderItem) {
      return orderItem.Id();
    });
    var childOrderItemsToRemove = viewModel.orderItems().filter(function (orderItem) {
      return !!orderItem.ParentOrderItemId() && orderItemIdsToRemove.indexOf(orderItem.ParentOrderItemId()) !== -1;
    });
    window._.union(childOrderItemsToRemove, orderItemsToRemove).forEach(function (orderItem) {
      viewModel.orderItems().splice(viewModel.orderItems().indexOf(orderItem), 1);
      window.database.remove(orderItem.innerInstance);
    });
    var alternativeSelectionWasRemoved = viewModel.alternativeSelectionFor() !== null && viewModel.orderItems().indexOf(viewModel.alternativeSelectionFor()) === -1;
    if (alternativeSelectionWasRemoved) {
      viewModel.alternativeSelectionFor(null);
    }
    d.resolve();
    return d.promise();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.addVariableValueAsOrderItem = function (variableValue) {
    var viewModel = this;
    var orderItem = window.ko.utils.arrayFirst(viewModel.orderItems(), function (x) { return x.ArticleId() === variableValue.ChildId(); });
    if (!orderItem) {
      orderItem = window.database.CrmOrder_OrderItem.CrmOrder_OrderItem.create().asKoObservable();
      window.database.add(orderItem);
      orderItem.Id(window.$data.createGuid().toString().toLowerCase());
      orderItem.Discount(0);
      orderItem.ArticleId(variableValue.ChildId());
      orderItem.ArticleNo(variableValue.Child().ItemNo());
      orderItem.ArticleDescription(variableValue.Child().Description());
      orderItem.ArticleHasAccessory(variableValue.Child().HasAccessory());
      orderItem.OrderId(viewModel.baseOrder().Id());
      orderItem.Price(variableValue.ExtensionValues().SalesPrice() || variableValue.Child().Price());
      orderItem.PurchasePrice(variableValue.ExtensionValues().PurchasePrice() || variableValue.Child().PurchasePrice());
      orderItem.QuantityUnitKey(variableValue.Child().QuantityUnitKey());
      orderItem.QuantityValue(1);
      orderItem.VATLevelKey(variableValue.Child().VATLevelKey());
      viewModel.orderItems().push(orderItem);
    }
    orderItem.ExtensionValues().VariableId(variableValue.ParentId());
    orderItem.IsOption(viewModel.isOption(variableValue));
    var isAlternative = viewModel.alternativeSelectionFor() !== null && viewModel.selectedVariableValues.indexOf(variableValue.ChildId()) !== -1;
    if (isAlternative) {
      orderItem.IsAlternative(true);
      orderItem.ParentOrderItemId(viewModel.alternativeSelectionFor().Id());
    } else {
      orderItem.IsAlternative(false);
      orderItem.ParentOrderItemId(null);
    }
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.confirmApplyRules = function (articleIdsToAdd, articleIdsToRemove) {
    var viewModel = this;
    var d = new $.Deferred();
    var showConfirmDialog = articleIdsToAdd.length + articleIdsToRemove.length > 1;
    if (!showConfirmDialog) {
      return d.resolve().promise();
    } else {
      var activeModal = $(".modal:visible");
      if (activeModal.length > 0) {
        activeModal.hide();
      }
      window.database.CrmArticle_Article.filter(function (x) {
        return x.Id in this.ids;
      }, { ids: window._.union(articleIdsToAdd, articleIdsToRemove) })
		  .toArray(function (articles) {
		    var message = window.Helper.String.getTranslatedString("RequiredChangesToApplySelection");
		    if (articleIdsToAdd.length > 0) {
		      var articlesToAdd = articles.filter(function (article) { return articleIdsToAdd.indexOf(article.Id) !== -1; });
		      var articlesToAddText = "\r\n" + articlesToAdd.map(function (article) { return article.ItemNo + " - " + article.Description; }).join("\r\n");
		      message += "\r\n\r\n" + window.Helper.String.getTranslatedString("ToConfigurationWillBeAdded").replace("{0}", articlesToAddText);
		    }
		    if (articleIdsToRemove.length > 0) {
		      var articlesToRemove = articles.filter(function (article) { return articleIdsToRemove.indexOf(article.Id) !== -1; });
		      var articlesToRemoveText = "\r\n" + articlesToRemove.map(function (article) { return article.ItemNo + " - " + article.Description; }).join("\r\n");
		      message += "\r\n\r\n" + window.Helper.String.getTranslatedString("FromConfigurationWillBeRemoved").replace("{0}", articlesToRemoveText);
		    }
		    window.swal({
		      title: window.Helper.String.getTranslatedString("ApplySelection"),
		      text: message,
		      type: "warning",
		      showCancelButton: true,
		      confirmButtonText: window.Helper.String.getTranslatedString("Apply"),
		      cancelButtonText: window.Helper.String.getTranslatedString("Cancel")
		    }, function (isConfirm) {
		      if (isConfirm) {
		        d.resolve();
		      } else {
		        d.reject();
		        if (activeModal.length > 0) {
		          activeModal.show();
		        }
		      }
		    });
		  }).fail(d.reject);
    }
    return d.promise();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.showVariableValueSelection = function (variable, isDefault) {
    var viewModel = this;
    var d = new $.Deferred();
    viewModel.loading(true);
    var configurationTab = $(".tab-nav a[href='#tab-configuration']");
    configurationTab.tab("show");
    viewModel.variableValueFilterByIsDefault(typeof isDefault === "boolean" ? isDefault : null);
    viewModel.variableValueVariableId(variable === null ? null : variable.Id());
    viewModel.selectedVariableValues(viewModel.getOrderItemsForVariable(variable).map(function (orderItem) { return orderItem.ArticleId() }));
    viewModel.selectedOptionalVariableValues(viewModel.getOptionalOrderItemsForVariable(variable).map(function (orderItem) { return orderItem.ArticleId() }));
    var $modal = $("#variable-value-selection");
    $modal.modal();
    $modal.one("hidden.bs.modal", function () {
      viewModel.selectedVariableValues([]);
      viewModel.selectedOptionalVariableValues([]);
      viewModel.variableValueFilter("");
      viewModel.variableValueFilterByIsDefault(null);
      viewModel.variableValueSelectionPager.page(1);
      viewModel.variableValueSelectionPager.totalItemCount(null);
      viewModel.variableValueVariableId(null);
    });
    d.resolve();
    viewModel.loading(false);
    return d.promise();
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getOrderItemsForVariable = function (variable) {
    var viewModel = this;
    return window.ko.utils.arrayFilter(viewModel.orderItems(), function (orderItem) {
      var result = true;
      if (viewModel.alternativeSelectionFor() !== null) {
        result = orderItem.ParentOrderItemId() === viewModel.alternativeSelectionFor().Id();
      } else {
        result = orderItem.ParentOrderItemId() === null;
      }
      result = result && (variable === null || orderItem.ExtensionValues().VariableId() === variable.Id());
      return result;
    });
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getOptionalOrderItemsForVariable = function (variable) {
    var viewModel = this;
    return window.ko.utils.arrayFilter(viewModel.orderItems(), function (orderItem) {
      var result = true;
      if (viewModel.alternativeSelectionFor() !== null) {
        result = false;
      }
      result = result && (variable === null || orderItem.ExtensionValues().VariableId() === variable.Id());
      result = result && orderItem.IsOption();
      return result;
    });
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.isOption = function (variableValue) {
    var viewModel = this;
    var index = viewModel.selectedOptionalVariableValues.indexOf(variableValue.ChildId());
    return index !== -1;
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.toggleIsOption = function (variableValue) {
    var viewModel = this;
    if (viewModel.isOption(variableValue)) {
      viewModel.selectedOptionalVariableValues.remove(variableValue.ChildId());
    } else {
      if (viewModel.selectedVariableValues.indexOf(variableValue.ChildId()) === -1) {
        viewModel.selectedVariableValues.push(variableValue.ChildId());
      }
      viewModel.selectedOptionalVariableValues.push(variableValue.ChildId());
    }
  }
  var baseRemoveOrderItem = namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.removeOrderItem;
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.removeOrderItem = function (orderItem) {
    var viewModel = this;
    if (viewModel.baseOrder().OrderEntryType() !== "Configuration") {
    	return baseRemoveOrderItem.apply(viewModel, arguments);
    }
    if (viewModel.requiredVariableValueArticleIds().indexOf(orderItem.ArticleId()) !== -1) {
      viewModel.showArticleRequiredMessage();
      return new $.Deferred().reject().promise();
    }

    var orderItemArticleIdsWithRules = window.ko.observableArray(window.ko.utils.arrayFilter(viewModel.orderItems(), function (x) {
      return x.IsOption() === false;
    }).map(function (x) { return x.ArticleId(); }));
    var deselectedOrderItemArticleIds = [orderItem.ArticleId()];
    orderItemArticleIdsWithRules.removeAll(deselectedOrderItemArticleIds);
    var articleIdsAfterRules = window.ko.observableArray(viewModel.applyRules(orderItemArticleIdsWithRules(), [], true));
    var articleIdsToAdd = articleIdsAfterRules().filter(function (x) {
      return orderItemArticleIdsWithRules().indexOf(x) === -1;
    });
    var articleIdsToRemove = window._.union(orderItemArticleIdsWithRules().filter(function (x) {
      return articleIdsAfterRules().indexOf(x) === -1;
    }), deselectedOrderItemArticleIds);
    return viewModel.confirmApplyRules(articleIdsToAdd, articleIdsToRemove).pipe(function () {
      return viewModel.addArticleIds(articleIdsToAdd);
    }).pipe(function () {
      return viewModel.removeArticleIds(articleIdsToRemove);
	}).pipe(function () {
			viewModel.orderItems.valueHasMutated();
      return baseRemoveOrderItem.apply(viewModel, arguments);
    }).fail(function () {
      // TODO: handle errors
    });
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.showArticleRequiredMessage = function () {
    window.swal({
      title: window.Helper.String.getTranslatedString("ArticleRequiredTitle"),
      text: window.Helper.String.getTranslatedString("ArticleRequiredMessage"),
      type: "error",
      confirmButtonText: window.Helper.String.getTranslatedString("Close"),
      confirmButtonClass: "btn-danger"
    });
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.applyRules = function (articleIds, preferredArticleIds, remove) {
    var viewModel = this;
    var result = articleIds.slice();

    function intersect(array1, array2) {
      return array1.filter(function (n) {
        return array2.indexOf(n) !== -1;
      });
    };

    function difference(array1, array2) {
      return array1.filter(function (n) {
        return array2.indexOf(n) === -1;
      });
    };

    viewModel.configurationRules()
		.sort(function (a, b) {
		  if (intersect(preferredArticleIds, a.AffectedVariableValues).length > 0) {
		    return 1;
		  }
		  if (intersect(preferredArticleIds, b.AffectedVariableValues).length > 0) {
		    return -1;
		  }
		  return 0;
		})
		.forEach(function (rule) {
		  if (remove !== true && intersect(result, rule.VariableValues).length > 0) {
		    result = window.Crm.Configurator.Rules[rule.Validation].applyAdd(result, rule);
		  } else if (remove === true && intersect(result, rule.VariableValues).length > 0) {
		    result = window.Crm.Configurator.Rules[rule.Validation].applyRemove(result, rule);
		  }
		});
    var rulesWereApplied = result.length !== articleIds.length || difference(result, articleIds).length > 0 || difference(articleIds, result).length > 0;
    if (rulesWereApplied) {
      return viewModel.applyRules(result, preferredArticleIds, remove);
    }
    return result;
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getRulesForArticleId = function (articleId) {
    var viewModel = this;
    var rulesForArticleId = window.ko.utils.arrayFilter(viewModel.configurationRules(), function (rule) {
      return rule.VariableValues.indexOf(articleId) !== -1;
    });
    return rulesForArticleId;
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.groupRulesByValidation = function (rules) {
    var result = {};
    rules.forEach(function (rule) {
      result[rule.Validation] = result[rule.Validation] || [];
      result[rule.Validation] = window._.union(result[rule.Validation], rule.AffectedVariableValues);
    });
    return result;
  }
  namespace("Crm.Order.ViewModels").BaseOrderDetailsViewModel.prototype.getMatchingAddRules = function (articleId) {
    var viewModel = this;
    var lastMatchingAddRules = [];
    return window.ko.pureComputed(function () {
      var groupedRules = viewModel.groupRulesByValidation(viewModel.getRulesForArticleId(articleId));
      var matchingAddRules = [];
      var orderArticleIds = window.ko.utils.arrayFilter(viewModel.orderItems(), function (x) {
        return x.IsOption() === false && x !== viewModel.alternativeSelectionFor();
      }).map(function (x) { return x.ArticleId(); });
      var selectedArticleIds = window.ko.utils.arrayFilter(viewModel.variableValues(), function (x) {
        return viewModel.selectedVariableValues().indexOf(x.ChildId()) !== -1 && viewModel.selectedOptionalVariableValues().indexOf(x.ChildId()) === -1 && orderArticleIds.indexOf(x.ChildId()) === -1;
      }).map(function (x) { return x.ChildId(); });
      var articleIds = orderArticleIds.concat(selectedArticleIds);
      Object.getOwnPropertyNames(groupedRules).forEach(function (validation) {
        var matchingArticleIds = window.Crm.Configurator.Rules[validation].getAddRuleMatches(articleId, groupedRules[validation], articleIds);
        var lastResultIndex = lastMatchingAddRules.findIndex(function (x) { return x.validation === validation; });
        if (matchingArticleIds.length > 0) {
	        var resultChanged = lastResultIndex === -1 || !window._.isEqual(lastMatchingAddRules[lastResultIndex].matchingArticleIds, matchingArticleIds);
	        if (resultChanged) {
		        var alertCssClass = window.ko.observable(window.Crm.Configurator.Rules[validation].getAlertCssClass());
		        var alertMessage = window.ko.observable(window.Crm.Configurator.Rules[validation].getAlertMessage());
		        var matchingArticles = window.ko.observableArray([]);
		        matchingAddRules.push({
			        css: alertCssClass,
			        message: alertMessage,
			        matchingArticleIds: matchingArticleIds,
			        matchingArticles: matchingArticles,
			        validation: validation
		        });
		        window.database.CrmArticle_Article.filter(function(x) {
				        return x.Id in this.ids;
			        }, { ids: matchingArticleIds })
			        .toArray(function(results) {
				        alertMessage(window.Crm.Configurator.Rules[validation].getAlertMessage(results));
				        matchingArticles(results);
			        });
	        } else {
			    matchingAddRules.push(lastMatchingAddRules[lastResultIndex]);
	        }
        }
      });
      lastMatchingAddRules = matchingAddRules;
      return matchingAddRules;
    });
  }
});