namespace WebApplication.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ConsumeTypeModel ConsumeType { get; set; }
        public double PriceKwh { get; set; }
        public double PricePeriod { get; set; }
        public int LimitPackage { get; set; }
        public double PricePackage { get; set; }

        public bool IsValid()
        {
            if (this.ConsumeType == ConsumeTypeModel.Undefined)
            {
                return false;
            }

            if (this.PriceKwh <= 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }

            if (this.ConsumeType == ConsumeTypeModel.Basic)
            {
                return BasicIsValid();
            }

            return PackageIsValid();
        }

        public bool BasicIsValid()
        {
            if (this.PricePeriod <= 0)
            {
                return false;
            }

            return true;
        }

        public bool PackageIsValid()
        {
            if (this.LimitPackage <= 0)
            {
                return false;
            }

            if (this.PricePackage <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
