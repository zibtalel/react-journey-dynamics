namespace Crm.PerDiem.Model
{
	using System;

	public class NonClosedExpensePeriod
	{
		public string Username { get; set; }
		public DateTime FirstDate { get; set; }
		public DateTime LastDate { get; set; }
		public PerDiemReport PerDiemReport { get; set; }

		protected bool Equals(NonClosedExpensePeriod other)
		{
			return string.Equals(Username, other.Username) && FirstDate.Equals(other.FirstDate) && LastDate.Equals(other.LastDate);
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			return Equals((NonClosedExpensePeriod)obj);
		}
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Username != null ? Username.GetHashCode() : 0;
				hashCode = (hashCode * 397) ^ FirstDate.GetHashCode();
				hashCode = (hashCode * 397) ^ LastDate.GetHashCode();
				return hashCode;
			}
		}
	}
}
