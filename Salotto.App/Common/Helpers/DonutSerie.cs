namespace Salotto.App.Common.Helpers
{
    public class DonutSerie
    {
        public DonutSerie(string category, int value, string color)
        {
            this.category = category;
            this.value = value;
            this.color = color;
        }

        public string category { get; set; }
        public int value { get; set; }
        public string color { get; set; }
    }
}
