namespace Sms.Einsatzplanung.Connector.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Attributes;
	using Crm.Library.Model;
	using Crm.Model;

	using Newtonsoft.Json;

	public class UserExtension : EntityExtension<User>
	{
		[UI(UIignore = true), JsonIgnore]
		public virtual Address HomeAddress { get; set; }

		[UI(Hidden = true)]
		public virtual Guid? HomeAddressId { get; set; }

		private string street;
		[JsonIgnore, UI(UIignore = true)]
		public virtual string Street
		{
			get => HomeAddress?.Street ?? street;
			set
			{
				if (HomeAddress != null)
				{
					HomeAddress.Street = value;
				}
				else
				{
					street = value;
				}
			}
		}

		private string zipCode;
		[JsonIgnore, UI(UIignore = true)]
		public virtual string ZipCode
		{
			get => HomeAddress?.ZipCode ?? zipCode;
			set
			{
				if (HomeAddress != null)
				{
					HomeAddress.ZipCode = value;
				}
				else
				{
					zipCode = value;
				}
			}
		}

		private string city;
		[JsonIgnore, UI(UIignore = true)]
		public virtual string City
		{
			get => HomeAddress?.City ?? city;
			set
			{
				if (HomeAddress != null)
				{
					HomeAddress.City = value;
				}
				else
				{
					city = value;
				}
			}
		}

		private string countryKey;
		[LookupKey, JsonIgnore, UI(UIignore = true)]
		public virtual string CountryKey
		{
			get => HomeAddress?.CountryKey ?? countryKey;
			set
			{
				if (HomeAddress != null)
				{
					HomeAddress.CountryKey = value;
				}
				else
				{
					countryKey = value;
				}
			}
		}

		public virtual string PublicHolidayRegionKey { get; set; }
	}
}
