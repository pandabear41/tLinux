using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Text;

namespace Terraria
{
	class PluginManager
	{
        public static void Main()
        {
            try
            {
                // Production code would usually load the directory from config
                var catalog = new DirectoryCatalog(@"..\..\..\Plugins");
                var container = new CompositionContainer(catalog);
                var plugiw
                {
                    CalculationServices = container.GetExportedValues<ICalculationService>()
                };

                foreach (var calculationService in pluginRepository.CalculationServices)
                {
                    const int number1 = 10;
                    const int number2 = 20;
                    int result = calculationService.Calculate(number1, number2);

                    string output = string.Format("calculationService.Calculate({0},{1}) = {2}", number1, number2, result);

                    Console.WriteLine(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

	}
}
