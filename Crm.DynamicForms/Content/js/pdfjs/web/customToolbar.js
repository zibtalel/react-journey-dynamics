﻿let sheet = (function () {
    let style = document.createElement("style");
    style.appendChild(document.createTextNode(""));
    document.head.appendChild(style);
    return style.sheet;
})();
function editToolBar() {
    //when the page is resized, the viewer hides and move some buttons around.
    //this function forcibly show all buttons so none of them disappear or re-appear on page resize
    removeGrowRules();


    /* Hiding elements */
    removeElement('viewFind');
    removeElement('secondaryToolbarToggle')
    removeElement('openFile')
    removeElement('print')
    removeElement('download')
    removeElement('viewBookmark')
    removeElement('secondaryOpenFile');
    removeElement('sidebarToggle');
}
function changeIcon(elemID, iconUrl) {
    let element = document.getElementById(elemID);
    let classNames = element.className;
    classNames = elemID.includes('Toggle') ? 'toolbarButton#' + elemID :
        classNames.split(' ').join('.');
    classNames = elemID.includes('view') ? '#' + elemID + '.toolbarButton' : '.' + classNames
    classNames += "::before";
    addCSSRule(sheet, classNames, `content: url(${iconUrl}) !important`, 0)
}
function addElemFromSecondaryToPrimary(elemID, parentID) {
    let element = document.getElementById(elemID);
    let parent = document.getElementById(parentID);
    element.style.minWidth = "0px";
    element.innerHTML = ''
    parent.append(element);
}
function removeElement(elemID) {
  let element = document.getElementById(elemID);
  if (element !== null) {
    element.parentNode.removeChild(element);
  }
}
function removeGrowRules() {
    addCSSRule(sheet, '.hiddenSmallView *', 'display:block !important');
    addCSSRule(sheet, '.hiddenMediumView', 'display:block !important');
    addCSSRule(sheet, '.hiddenLargeView', 'display:block !important');
    addCSSRule(sheet, '.visibleSmallView', 'display:block !important');
    addCSSRule(sheet, '.visibleMediumView', 'display:block !important');
    addCSSRule(sheet, '.visibleLargeView', 'display:block !important');
}
function addCSSRule(sheet, selector, rules, index) {
    if ("insertRule" in sheet) {
        sheet.insertRule(selector + "{" + rules + "}", index);
    }
    else if ("addRule" in sheet) {
        sheet.addRule(selector, rules, index);
    }
}
window.onload = editToolBar