using Newtonsoft.Json;
using System.Text;
using ForBeauty.Models;
using System.Net.Http.Headers;
using ForBeautyMaui;
using ForBeautyMaui.ApiServices;

namespace ForBeautyMaui.ApiServices
{
	public class ApiSerives : ContentPage
	{

		public static async Task<(bool check ,string result)> CheckUser(string phonenumber)
		{
			var user = new Register()
			{
				PhoneNumber = phonenumber
			};
			HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(user);
			var contect = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/user", contect);
			var result = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				return (false, result);
			}
			else
			{
				return (true, null);
			}

		}

		public static async Task<string> Confirm(Confirm Con)
		{
			HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(Con);
			var contect = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/VerifyCode", contect);
			var responseJ = await response.Content.ReadAsStringAsync();
			return responseJ;
		}

		public static async Task<bool> Register(string FName, string LName, string password, string phoneN, string CurrentDevice)
		{
			HttpClient httpClient = new HttpClient();
			var account = new AccountnDeatil()
			{
				FName = FName,
				LName = LName,
				DeviceModel = CurrentDevice,
				Password = password,
				PhoneNumber = phoneN,




			};
			var json = JsonConvert.SerializeObject(account);
			var contect = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/Register", contect);
			if (!response.IsSuccessStatusCode) return false;
			return true;
		}
		public static async Task<(bool Check , string Message)> Login(string PhoneNumber, string Password)
		{
			var user = new Login()
			{
				PhoneNumber = PhoneNumber,
				Password = Password
			};
			HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(user);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/Login", content);
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return (false,result);
			
			var jsonresult = JsonConvert.DeserializeObject<TokenReponse>(result);
			Preferences.Set("access_token", jsonresult.access_token);
			Preferences.Set("user_Id", jsonresult.user_Id);
			Preferences.Set("user_name", jsonresult.user_name);
			Preferences.Set("creation_Time", jsonresult.creation_Time);
			Preferences.Set("expiration_Time", jsonresult.expiration_Time);
			Preferences.Set("currentTime", UnixTime.GetCurrentTime());

            return (true,null);


		}
		public static async Task<List<Categories>> CategorySearch()
		{
			await TokenValidator.CheckTokenValidity();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Categories/GetCategory");
			return JsonConvert.DeserializeObject<List<Categories>>(response);

		}
		public static async Task<List<Product>> GetProductCategory(string CategoryName, int CategoryId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/" + CategoryName + "/" + CategoryId);
			return JsonConvert.DeserializeObject<List<Product>>(response);
		}
		public static async Task<List<Categories>> GetPoupluarCategory()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Categories/GetPoupluarCategory");
			return JsonConvert.DeserializeObject<List<Categories>>(response);
		}

		public static async Task<List<Product>> GetAllProducts()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetAllProducts");
			return JsonConvert.DeserializeObject<List<Product>>(response);
		}
		public static async Task<List<CategorDital>> CategorDitals(int CategorId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Categories/" + CategorId);
			return JsonConvert.DeserializeObject<List<CategorDital>>(response);
		}
		public static async Task<List<ImageModel>> GetDitalImages(int ProductId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetDitalImages/" + ProductId);
			return JsonConvert.DeserializeObject<List<ImageModel>>(response);
		}
		public static async Task<List<OtherDesginProduct>> OtherDesginP(int productId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var respnse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetOtherDProduct/" + productId);
			return JsonConvert.DeserializeObject<List<OtherDesginProduct>>(respnse);
		}
		public static async Task<List<AddToFavourite>> GetFavouritesByuser(int userId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "AddToFavourites/GetFavouriteUser/" + userId);
			return JsonConvert.DeserializeObject<List<AddToFavourite>>(response);
		}
		public static async Task<bool> PostFavourite(AddToFavourite addTo)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(addTo);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var respnse = await httpClient.PostAsync(ApiU.ApiUrl + "AddToFavourites/PostAddToFav/", content);
			if (!respnse.IsSuccessStatusCode) return false;
			return true;

		}

		public static async Task<bool> CheckProductInFavorites(CheckFavourite check)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(check);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var respnse = await httpClient.PostAsync(ApiU.ApiUrl + "AddToFavourites/CheckProductInFavorites", content);
			if (!respnse.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<bool> RemoveFromFavorites(int userId, int productId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
            HttpResponseMessage response = await httpClient.DeleteAsync(ApiU.ApiUrl + $"AddToFavourites/user/{userId}/product/{productId}");

			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static async Task<bool> AddToShoppingCart(ShoppingCart AddShop)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(AddShop);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var respnse = await httpClient.PostAsync(ApiU.ApiUrl + "ShoppingCart/PostInShoppingCart", content);
			if (!respnse.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<List<ShoppingCart>> GetShoppingCartItem(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",Preferences.Get("access_token", string.Empty));
            var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "ShoppingCart/GetShoppingCartByUser/" + UserId);
			return JsonConvert.DeserializeObject<List<ShoppingCart>>(response);
		}
		public static async Task<bool> RemoveFromShoppingCart(int UserId, int ProductId, string Size)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",Preferences.Get("access_token", string.Empty));
			var response = await httpClient.DeleteAsync(ApiU.ApiUrl + "ShoppingCart/" + UserId + "/" + ProductId + "/" + Size);
			if (!response.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<(bool check , Order order , string Error)> PlaceOrder(Order placeOrder)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(placeOrder);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Orders", content);
			var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return (false, null, result);
            var Fresult =JsonConvert.DeserializeObject<Order>(result);
			return (true,Fresult,null);
		}
		public static async Task<UserInformation> GetPersonalDeatil(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Users/GetUserInformation/" + UserId);
			return JsonConvert.DeserializeObject<UserInformation>(response);
		}
		public static async Task<bool> UpdateUserInformation(int UserId, UserInformation user)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(user);
			var conteent = new StringContent(json, Encoding.UTF8, "application/json");
			var respnse = await httpClient.PutAsync(ApiU.ApiUrl + "Users/updateUserName/" + UserId, conteent);
			if (!respnse.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<List<BestSearch>> GetSearchProduct()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var respnse = await httpClient.GetStringAsync(ApiU.ApiUrl + "products/GetBestSearchProduct");
			return JsonConvert.DeserializeObject<List<BestSearch>>(respnse);

		}
		public static async Task<List<Categories>> GetBestCategories()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var respnse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Categories/GetBestCategories");
			return JsonConvert.DeserializeObject<List<Categories>>(respnse);

		}
		public static async Task<List<Product>> GetNewIn()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var respnse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetNewInProduct");
			return JsonConvert.DeserializeObject<List<Product>>(respnse);

		}
		public static async Task<bool> AddFromFavourites(ShoppingCart AddFromFavourites)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(AddFromFavourites);
			var contect = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "AddToFavourites/AddFromFaviourtes", contect);
			if (!response.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<List<Product>> GetDiscountProduct()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetDiscountProduct");
			return JsonConvert.DeserializeObject<List<Product>>(response);

		}
		public static async Task<List<Product>> ShowBestCategoryProduct(int CategorId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var reponse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/ShowBestCategoryProduct/" + CategorId);
			return JsonConvert.DeserializeObject<List<Product>>(reponse);

		}
		public static async Task<List<Product>> GetGiftBox()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/ShowInGiftProduct");
			return JsonConvert.DeserializeObject<List<Product>>(response);

		}
		public static async Task<ShoppingCart> UpdateQyt(ShoppingCart update, int productid, string size)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(update);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var reponse = await httpClient.PutAsync(ApiU.ApiUrl + "ShoppingCart/UpdateQyt/" + productid + "/" + size, content);
			var reslut = await reponse.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ShoppingCart>(reslut);
		}
		public static async Task<string> NumberOfCategory(int CategorId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var reponse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/NumberOfProduct/" + CategorId);
			return JsonConvert.DeserializeObject<string>(reponse);
		}
		public static async Task<List<PrefeuseOrderUserById>> PrevouseOrderByUser(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Orders/OrderByUser/" + UserId);
			return JsonConvert.DeserializeObject<List<PrefeuseOrderUserById>>(response);


		}
		public static async Task<List<OrderDetail>> GetOrderDetails(int orderId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Orders/OrderDetails/" + orderId);
			return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
		}
		public static async Task<List<Product>> SeeMoreProduct(int CategorId, int ProductId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var reponse = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/SeeMoreProducts/" + CategorId+"/"+ProductId);
			return JsonConvert.DeserializeObject<List<Product>>(reponse);
		}
		public static async Task<bool> UsersVist(UserVist user)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(user);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var reponse = await httpClient.PostAsync(ApiU.ApiUrl + "Users/UserVist", content);
			if (!reponse.IsSuccessStatusCode) return false;
			return true;
		}
		public static async Task<bool> PostRecentlyViewd(RecentlyViewd viewd)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(viewd);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Products/recentlyViewd", content);
			if (!response.IsSuccessStatusCode) return false;
			return true;

		}
		public static async Task<List<Product>> GetRecentlyViewd(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetRecentlyVied/" + UserId);
			return JsonConvert.DeserializeObject<List<Product>>(response);
		}
		public static async Task<List<BrandsName>> GetBrandsName(int Id)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetFaumousBrandsName/" + Id);
			var brandNames = JsonConvert.DeserializeObject<List<BrandsName>>(response);
			return brandNames;

		}
		public static async Task<List<Product>> GetProductByBrand(string BrandName)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetProductByBrand/" + BrandName);
			return JsonConvert.DeserializeObject<List<Product>>(response);
		}
		public static async Task<List<Cities>> GetCites()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "ShoppingCart/GetCitesName");
			return JsonConvert.DeserializeObject<List<Cities>>(response);

		}
		public static async Task<List<Area>> GetAreaByCity(int CityId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "ShoppingCart/GetAreaByCity/" + CityId);
			return JsonConvert.DeserializeObject<List<Area>>(response);

		}
		public static async Task<(bool success, string result)> ChangePassword(ChangePassword change)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(change);
			var contect = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PutAsync(ApiU.ApiUrl + "Users/ChangePassword", contect);
			var result = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				return (false, result);
			}
			else
			{
				return (true, result);
			}

		}
		public static async Task<(bool check, CheckCoupon message, string checkcoupon)> CheckCoupon(CheckCoupon checkCoupon)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var json = JsonConvert.SerializeObject(checkCoupon);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Coupons/CheckCouponsDataUsers", content);
			var jsonresponse = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				return (false, null, jsonresponse);
			}

			try
			{
				CheckCoupon result = JsonConvert.DeserializeObject<CheckCoupon>(jsonresponse);
				return (true, result, jsonresponse);
			}
			catch (JsonSerializationException)
			{
				List<CheckCoupon> resultArray = JsonConvert.DeserializeObject<List<CheckCoupon>>(jsonresponse);
				return (true, resultArray.FirstOrDefault(), "");
			}
		}
		public static async Task<(bool check ,CheckLimt checklimt)> CheckCouponLimt(string coupon)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetAsync(ApiU.ApiUrl +"Coupons/CheckCouponLimt/"+coupon);
			var result = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
				return (false, null);
			else
			{
				var result1 = JsonConvert.DeserializeObject<CheckLimt>(result);
				return (true, result1);
			}
		


        }
		public static async Task<List<Product>> ShowAllNewIn()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response =await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetAllNewInProduct");
			return JsonConvert.DeserializeObject<List<Product>>(response);
        }
		public static async Task<OrderAllawAndLimt> CheckAllawAndLimt()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response =await httpClient.GetAsync(ApiU.ApiUrl + "Orders/GetAllawOrderAndLimt");
			var result = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OrderAllawAndLimt>(result);
		
        }

		public static async Task<bool> IsBlocked(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetAsync(ApiU.ApiUrl + "Users/CheckBlockUser/" + UserId);
			if (!response.IsSuccessStatusCode) return false;
			return true;
        }
		public static async Task<bool> GetPushNotificationTokenUser(PushNotificationUserToken PostToken)
		{
			HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(PostToken);
			var content = new StringContent(json, Encoding.UTF8,"application/json");
			var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/GetPushNotificationTokenFromUser", content);
			if (!response.IsSuccessStatusCode) return false;
			return true;
        }
        public static async Task<bool> CheckGiftProduct(int ProductId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
            var response = await httpClient.GetAsync(ApiU.ApiUrl + "Products/CheckGiftProduct/"+ ProductId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<List<Product>>SearchProduct(string Search)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/SearchProduct/"+Search);
			return JsonConvert.DeserializeObject<List<Product>>(response);
        }
        public static async Task<List<Product>> GetBestSearchProduct(string name)
        {
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
            var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetBestSearch/" + name);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }
        public static async Task<List<Product>> GetExclosveProduct()
        {
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
            var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetExclosveProduct");
            return JsonConvert.DeserializeObject<List<Product>>(response);

        }
		public static async Task<ExclosveDesgin> GetExclosveDesgin()
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
            var response = await httpClient.GetStringAsync(ApiU.ApiUrl + "Products/GetExclosveDesgin");
            return JsonConvert.DeserializeObject<ExclosveDesgin>(response);
        }
		public static async Task<(bool state ,OrderState OrderResult)> GetOrderStateUser(int UserId)
		{
            await TokenValidator.CheckTokenValidity();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("access_token", string.Empty));
			var response =await httpClient.GetAsync(ApiU.ApiUrl + "Orders/OrderStateByUser/"+UserId);
			if (!response.IsSuccessStatusCode) return (false, null);
            var result = await response.Content.ReadAsStringAsync();
            var fresult = JsonConvert.DeserializeObject<OrderState>(result);
			return (true, fresult);
        }
		public static async Task<bool> CheckUserDeviceId(UserDevice User)
		{
            
            HttpClient httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(User);
			var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync(ApiU.ApiUrl + "Users/CheckUserDeviceId/",content);
			var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }

	
}
public static class TokenValidator
{
	public async static Task CheckTokenValidity()
	{
		var ExpirationTime = Preferences.Get("expiration_Time", 0);
		Preferences.Set("currentTime", UnixTime.GetCurrentTime());
		var currentTime = Preferences.Get("currentTime", 0);
		if (ExpirationTime < currentTime)
		{
          await ApiSerives.Login(Preferences.Get("Phone_NumberForToken",string.Empty),Preferences.Get("Password_ForToken",string.Empty));

		}
	}
}
