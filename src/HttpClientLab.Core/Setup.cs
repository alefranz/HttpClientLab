using System.Collections.Generic;

namespace HttpClientLab
{

    public class Setup<TSetup>
    {
        protected Setup()
        {
            Setups = new List<TSetup>();
        }

        protected List<TSetup> Setups { get; set; }

        protected TSetup Add(TSetup setup)
        {
            Setups.Add(setup);
            return setup;
        }
    }
}
