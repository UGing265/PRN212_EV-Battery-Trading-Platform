using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBookingService
    {
        private DAL.EvbatterySwapDbContext repo = new();
        //public Booking? SearchOrders(int user_id, int product_id)
        //{
        //    List<Booking> allorders = this.GetAllOrders();
        //    for (int i = 0; i < allorders.Count; i++)
        //    {
        //        if (allorders[i].BuyerId == user_id && allorders[i].ProductId == product_id)
        //        {
        //            return allorders[i];
        //        }
        //    }
        //    return null;
        //}

        //public List<Booking> GetOrdersByUserId(int user_id)
        //{
        //    List<Booking> allorders = this.GetAllOrders();
        //    List<Booking> user_orders = new List<Booking>();
        //    for (int i = 0; i < allorders.Count; i++)
        //    {
        //        if (allorders[i].BuyerId == user_id)
        //        {
        //            user_orders.Add(allorders[i]);
        //        }
        //    }
        //    return user_orders;
        //}

        public List<Booking> GetAllOrders()
        {
            return repo.Bookings.ToList();
        }
    }
}
