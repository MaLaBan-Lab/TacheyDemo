using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachey001.Models;
using Tachey001.Repository;
using Tachey001.ViewModel;

namespace Tachey001.Service
{
    public class CheckoutService
    {
        private TacheyRepository _tacheyRepository;
        public CheckoutService()
        {
            _tacheyRepository = new TacheyRepository(new TacheyContext());
        }
        public CheckoutViewModel GetCheckoutInformation(string currentID)
        {
            var member = _tacheyRepository.GetAll<Member>();
            var finduser = from m in member
                           where m.MemberID == currentID
                           select new CheckoutViewModel
                           {
                               Name = m.Name,
                               Email = m.Email
                           };
            //要改成IQ型別才不用ToList
            var result = finduser.FirstOrDefault();
            return result;
        }
    }

}