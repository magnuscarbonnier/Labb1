using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class Lib
    {
        public const string SessionKeyCart = "_ShoppingCart";
        public const string SessionKeyTotalPrice = "_ShoppingTotalPrice";
        public const string SessionKeyOrder = "_ShoppingOrder";
        public enum Status { Beställd=0, Bekräftad=1, Packas=2, Skickad=3, Avbeställd=4}
    }
}
