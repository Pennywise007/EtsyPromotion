using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using ShopPromotion.UI;
using ShopPromotion.Promotion.Interfaces;
using ShopPromotion.Promotion.Implementation;


namespace ShopPromotion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(BuildServiceProvider().GetRequiredService<MainForm>());
        }

        /// <summary>
        /// Composition root.
        /// </summary>
        private static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<MainForm>();
            serviceCollection.AddTransient<IListingPromotionWorker, ListingPromotionWorker>();

            serviceCollection.AddTransient<IKeyWordPromotionForm, KeywordPromotionForm>();
            serviceCollection.AddTransient<IKeyWordPromotionWorker, KeyWordPromotionWorker>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
