namespace Sms.Einsatzplanung.Connector.Services
{
	using System;
	using System.Linq;

	using Crm.BackgroundServices;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.Library.Unicore;
	using Crm.Model;

	using Sms.Einsatzplanung.Connector.Model;

	public class HomeAddressPluginUserSaveExtension : IPluginUserSaveExtension
	{
		private readonly IRepositoryWithTypedId<Address, Guid> addressRepository;
		private readonly IUserService userService;
		private readonly Func<Address> addressFactory;
		private readonly IGeocodingService geocodingService;
		public HomeAddressPluginUserSaveExtension(IRepositoryWithTypedId<Address, Guid> addressRepository, IUserService userService, Func<Address> addressFactory, IGeocodingService geocodingService)
		{
			this.addressRepository = addressRepository;
			this.userService = userService;
			this.addressFactory = addressFactory;
			this.geocodingService = geocodingService;
		}
		public virtual Address GetHomeAddress(UserExtension userExtension, bool evict = false)
		{
			if (new[] { userExtension.CountryKey, userExtension.City, userExtension.Street, userExtension.ZipCode }.All(string.IsNullOrEmpty))
			{
				return null;
			}
			Address address;
			if (userExtension.HomeAddressId.HasValue)
			{
				address = addressRepository.Get(userExtension.HomeAddressId.Value);
				if (evict)
				{
					addressRepository.Session.Evict(address);
				}
			}
			else
			{
				address = addressFactory();
			}
			address.AddressTypeKey = "none";
			address.AuthData = address.AuthData ?? new LMobile.Unicore.EntityAuthData { DomainId = userExtension.ExtendedEntity.AuthData?.DomainId ?? UnicoreDefaults.CommonDomainId };
			address.CountryKey = userExtension.CountryKey;
			address.City = userExtension.City;
			address.Name1 = userExtension.ExtendedEntity.FirstName;
			address.Name2 = userExtension.ExtendedEntity.LastName;
			address.Street = userExtension.Street;
			address.ZipCode = userExtension.ZipCode;
			return address;
		}
		public virtual void Save(User user)
		{
			var userExtension = user.GetExtension<UserExtension>();
			Address address;
			if (userExtension.HomeAddress == null && (address = GetHomeAddress(userExtension)) != null)
			{
				geocodingService.TryGeocode(address);
				addressRepository.SaveOrUpdate(address);
				if (userExtension.HomeAddressId.HasValue == false)
				{
					userExtension.HomeAddressId = address.Id;
					userService.SaveUser(user);
				}
			}
		}
	}
}
