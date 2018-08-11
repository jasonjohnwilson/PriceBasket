using Bjss.PriceBasket.Core.Interfaces;
using Bjss.PriceBasket.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjss.PriceBasket.Core.Services
{
    /// <summary>
    /// Factory to seperate the creation of the classes used to determine the discounts and the class that processed them.
    /// New discounts can be added to the database and then an apprpriate class can be created to represent and calculate them
    /// </summary>
    public class BasketDiscountProviderFactory : IBasketDiscountProviderFactory
    {
        private readonly IGenericRepository<SpecialOffer> _specialOfferRepository;
        private readonly IFactory _objectFactory;

        public BasketDiscountProviderFactory(
            IGenericRepository<SpecialOffer> specialOfferRepository,
            IFactory objectFactory)
        {
            _specialOfferRepository = specialOfferRepository;
            _objectFactory = objectFactory;
        }

        /// <summary>
        /// Gets all discounts that can potentially be applied to a basket 
        /// </summary>
        /// <returns>Enumeration of basket discounts</returns>
        public async Task<IEnumerable<IBasketDiscounts>> GetAsync()
        {
            var basketDiscounts = new List<IBasketDiscounts>();

            foreach(var specialOffer in await _specialOfferRepository.GetAllAsync())
            {
                basketDiscounts.Add(_objectFactory.Get<IBasketDiscounts>(specialOffer.DiscountAssemblyFullName, _specialOfferRepository));
            }

            return basketDiscounts;
        }
    }
}
