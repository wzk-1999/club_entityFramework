
using zhankui_wang_Practice_Asst_5.Models;

namespace zhankui_wang_Practice_Asst_5.Utilities
{
    public static class CRUDutilities
    {
        public static Boolean UserExists(string userName)
        {
            using (var context = new PracticeAsst5DbContext())
            {
                return context.Users.Any(u => u.Name == userName);
            }
        }

        public static string ProvinceOfCity(string cityName)
        {
            using (var context = new PracticeAsst5DbContext())
            {
                var provinceName = (from city in context.Cities
                                    join province in context.Provinces
                                    on city.ProvCode equals province.Code
                                    where city.Name == cityName
                                    select province.Name).FirstOrDefault();

                return provinceName ?? "Province not found"; // Return a message if the city doesn't exist
            }
        }

        public static Boolean LivesInProv(string name, string prov)
        {
            using (var context = new PracticeAsst5DbContext())
            {
                var result = (from user in context.Users
                              join city in context.Cities
                              on user.CityName equals city.Name
                              join province in context.Provinces
                              on city.ProvCode equals province.Code
                              where user.Name == name && province.Name == prov
                              select user).Any();

                return result;
            }
        }
    }
}
