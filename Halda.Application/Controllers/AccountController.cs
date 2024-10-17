using Halda.Application.Models;
using Halda.Core.Const;
using Halda.Core.DTO;
using Halda.Core.DTO;
using Halda.Core.DTO;
using Halda.Core.DTO.Auth;
using Halda.Core.Models;
using Halda.Core.Models.Onboarding;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Services.Interface.ICompany;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Halda.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        protected readonly HttpClient _chitraclient;
        //protected readonly HttpClient _client;
        private readonly ILogger<AccountController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IUnitOfWork _unitOfWork;
        string baseUrl = "https://gtrbd.net/chitraupdateapi/api/";
        public AccountController(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork, ILogger<AccountController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _companyService = companyService;
            _unitOfWork = unitOfWork;

            //_client = _httpClientFactory.CreateClient("Halda");
            _chitraclient = _httpClientFactory.CreateClient("Chitra");
            _chitraclient.DefaultRequestHeaders.Add("Chitra-API-Key", "ChitraManechitramanebagbonerchitranakintoatagtrarchitra");

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Login(string? postId)
        {

            if (postId != null)
            {
                Response.Cookies.Append("PostId", postId);
                ViewBag.PostId = postId;
            }

            return View();
        }



        //Latest
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model, CancellationToken token)
        {
            try
            {
                if (model.UserName == null || model.Password == null)
                {
                    return View();
                }

                var result = await _chitraclient.PostAsJsonAsync("Auth/Login", model);
                int customStatusCode = 321;
                var httpResponse = new HttpResponseMessage((HttpStatusCode)customStatusCode);

                TokenResult resultToken = new TokenResult();

                if (result.IsSuccessStatusCode)
                {
                    resultToken = await result.Content.ReadFromJsonAsync<TokenResult>();
                }
                else if (result.StatusCode == httpResponse.StatusCode)
                {
                    TempData["UserName"] = model.UserName;
                    return RedirectToAction(nameof(ConfirmOTP));
                }
                else
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    var errorViewModel = JsonConvert.DeserializeObject<ErrorViewModel>(responseContent);
                    ModelState.AddModelError(string.Empty, errorViewModel?.error ?? "An error occurred during login.");
                    ViewBag.ErrorMessage = errorViewModel?.error ?? "An error occurred during login.";
                    return View();
                }

                //TempData["UserName"] = model.UserName;//Use for when want showing name in profile..

                ClaimsIdentity identity = await DecodeTokenAndCreateCookie(resultToken.access_token, HttpContext);
                Response.Cookies.Append("access_token", resultToken.access_token, new CookieOptions
                {
                    HttpOnly = true
                });
                Response.Cookies.Append("refresh_token", resultToken.Refresh_token, new CookieOptions
                {
                    HttpOnly = true
                });
                var roleClaims = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                string roles = string.Join(",", roleClaims);

                Response.Cookies.Append("user_roles", roles, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var ComId = identity.FindFirst(c => c.Type == "CompanyId")?.Value;

                Response.Cookies.Append("UserId", userId.ToString());
                Response.Cookies.Append("ComId", ComId.ToString());

                var userRole = Request.Cookies["user_roles"];
                if (Request.Cookies.ContainsKey("PostId") || userRole == "Applicant")
                {
                    if (Request.Cookies.ContainsKey("PostId"))
                    {
                        var postId = Request.Cookies["PostId"];
                        Response.Cookies.Delete("PostId");
                        return RedirectToAction("JobApplication", "Job", new { Id = postId });
                    }

                }
               // return RedirectToAction(nameof(CompanySync));
               // var employee = await GetEmployeeId(userId, ComId);
                //if (employee is not null)
                //{
                //    Response.Cookies.Append("EmpId", employee.Id);
                //    Response.Cookies.Append("EmpName", employee.FirstName + " " + employee.LastName);
                //}
                


                bool existiance = await _companyService.GetCompany(ComId, token);

                if (existiance != false)
                {
                    var response = await _chitraclient.GetAsync($"Company/CompanyInfo?comid={ComId}");

                    var content = await response.Content.ReadAsStringAsync();
                    var ComResult2 = JsonConvert.DeserializeObject<CompanyUpdateVM>(content);
                    TempData["CompanyName"] = ComResult2.Name;



                    //return RedirectToAction("Index", "Home", new { area = "Admin" });
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    //return RedirectToAction("Index", "Company");
                    return RedirectToAction("Index", "Home");

                }

                //if (Company.IsSuccessStatusCode && response.IsSuccessStatusCode)
                //{
                //    //return RedirectToAction("CreateCompany", "Company", new { area = "Admin", controller = "Company" });
                //    var Com = Company.Content.ReadAsStringAsync().Result;

                //    var ComResult = JsonConvert.DeserializeObject<CompanyDTO>(Com);

                //    if (ComResult == null || ComResult2.Name == "Default Company")
                //    {
                //        if (ComResult2.Name != "Default Company")
                //        {
                //            var userResponse = await _chitraclient.GetAsync($"UserCompany/Userinfo?id={userId}");
                //            var userContent = await userResponse.Content.ReadAsStringAsync();
                //            var userResult = JsonConvert.DeserializeObject<UserDTO>(userContent);

                //            CompanyDTO company = new CompanyDTO
                //            {
                //                Id = new string(ComResult2.Id),
                //                Email = userResult.Email,
                //                Name = ComResult2.Name,
                //                User = userResult
                //            };

                //            var companyResponse = await _client.PostAsJsonAsync("Company/Create", company);
                //            if (companyResponse.IsSuccessStatusCode)
                //            {
                //                TempData["CompanyName"] = ComResult2.Name;
                //            }
                //        }
                //        else
                //        {
                //            return RedirectToAction("CreateCompany", "Company", new { area = "Admin", controller = "Company" });
                //            //return RedirectToAction(nameof(CreateCompany), new { userId = userId, ComId = ComId });
                //        }
                //    }
                //}



            }

            catch (OperationCanceledException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred during login.");
                return View();
                throw;
            }
        }
        private async Task<Employee> GetEmployeeId(string userid,string comid)
        {
            return await _unitOfWork.employeeRepository.Single(x=>x.EmpUserId == userid && x.CompanyId==comid);
        }

        public async Task<IActionResult> CompanySync(CancellationToken token)
        {
            var comid = User.Claims.FirstOrDefault(x => x.Type == ChitraClaim.CompanyId)?.Value;
            var company = await _unitOfWork.companyRepository.Single(x => x.Id == comid);
            if (company is null)
            {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["access_token"]);
                var response = await client.GetAsync($"{baseUrl}UserCompany/UserCompanyInfo");
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var ComResult2 = JsonConvert.DeserializeObject<UserCompanyDto>(content);

                    var com = ComResult2.CompanyInfo;
                    if (com is not null)
                    {

                        var newCompany = new Company()
                        {
                            Id = comid,
                            CompanyName = com.Name,
                            comEmail = com.PhoneNumber,
                            comPhone = com.PhoneNumber,
                            SecoundaryAddress = com.AddresLinetwo,
                            PrimaryAddress = com.AddresLineOne
                        };
                        await _unitOfWork.companyRepository.AddAsync(newCompany);
                        await _unitOfWork.Save(token);
                    }
                }

            }
            await Task.CompletedTask;
            if (User.IsInRole("CompanyAdmin") && company.CompanyName=="Default Company" || string.IsNullOrEmpty(company.CompanyName))
            {
                return RedirectToAction("Index", "Company");
            }
            return RedirectToAction("Index", "Home");
        }
        //Old login
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginRequest model)
        //{
        //    if (model.UserName == null || model.Password == null)
        //    {
        //        return View();
        //    }

        //    // var result = await client.PostAsJsonAsync("https://localhost:7074/api/Auth/Login", model);
        //    var result = await _chitraclient.PostAsJsonAsync("Auth/Login", model);
        //    int customStatusCode = 321;
        //    var httpResponse = new HttpResponseMessage((HttpStatusCode)customStatusCode);

        //    TokenResult resultToken = new TokenResult();

        //    if (result.IsSuccessStatusCode)
        //    {
        //        resultToken = await result.Content.ReadFromJsonAsync<TokenResult>();
        //    }
        //    else if (result.StatusCode == httpResponse.StatusCode)
        //    {
        //        TempData["UserName"] = model.UserName;
        //        return RedirectToAction(nameof(ConfirmOTP));
        //    }
        //    else
        //    {
        //        var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
        //        ModelState.AddModelError(string.Empty, error.error);
        //        return View();
        //    }

        //    ClaimsIdentity identity = await DecodeTokenAndCreateCookie(resultToken.access_token, HttpContext);
        //    Response.Cookies.Append("access_token", resultToken.access_token, new CookieOptions
        //    {
        //        HttpOnly = true
        //    });
        //    Response.Cookies.Append("refresh_token", resultToken.Refresh_token, new CookieOptions
        //    {
        //        HttpOnly = true
        //    });

        //    // Access the ClaimTypes.NameIdentifier and ClaimTypes.Name from the ClaimsIdentity
        //    var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var ComId = identity.FindFirst(c => c.Type == "CompanyId")?.Value;



        //    //HttpContext.Items["UserId"] = userId.ToString();
        //    //HttpContext.Items["ComId"] = ComId;
        //    //var Email = identity.FindFirst(ClaimTypes.Name)?.Value;

        //    // Add UserId to session
        //    Response.Cookies.Append("UserId", userId.ToString());
        //    Response.Cookies.Append("ComId", ComId.ToString());

        //    var Company = _client.GetAsync("Company/GetById/" + ComId.ToString()).Result;

        //    var response = await _chitraclient.GetAsync($"Company/CompanyInfo?comid={ComId}");


        //    var content = await response.Content.ReadAsStringAsync();
        //    var ComResult2 = JsonConvert.DeserializeObject<CompanyUpdateVM>(content);

        //    if (Company.IsSuccessStatusCode && response.IsSuccessStatusCode)
        //    {
        //        var Com = Company.Content.ReadAsStringAsync().Result;

        //        var ComResult = JsonConvert.DeserializeObject<CompanyDTO>(Com);
        //        //var ComResult = await Company.Content.ReadFromJsonAsync<CompanyDTO>();
        //        //var content = await response.Content.ReadAsStringAsync();
        //        //var ComResult2 = JsonConvert.DeserializeObject<CompanyUpdateVM>(content);
        //        //var ComResult3 = JsonConvert.DeserializeObject<CreateCompanyDTO>(content);

        //        if (ComResult == null || ComResult2.Name == "Default Company")
        //        {
        //            if (ComResult2.Name != "Default Company")
        //            {
        //                //CompanyDTO
        //                var userResponse = await _chitraclient.GetAsync($"UserCompany/Userinfo?id={userId}");
        //                var userContent = await userResponse.Content.ReadAsStringAsync();
        //                var userResult = JsonConvert.DeserializeObject<UserDTO>(userContent);

        //                CompanyDTO company = new CompanyDTO
        //                {
        //                    Id = new string(ComResult2.Id),
        //                    Email = userResult.Email,
        //                    Name = ComResult2.Name,
        //                    User = userResult
        //                };

        //                var companyResponse = await _client.PostAsJsonAsync("Company/Create", company);
        //                if (companyResponse.IsSuccessStatusCode)
        //                {
        //                    TempData["CompanyName"] = ComResult2.Name;
        //                }

        //            }

        //            else
        //            {
        //                return RedirectToAction(nameof(CreateCompany), new { userId = userId, ComId = ComId });
        //            }

        //        }



        //    }


        //    //HttpContext.Session.SetString("Id", userId.ToString());
        //    //ChangeID changeID = new ChangeID
        //    //{
        //    //    UserId = new string(userId.ToString()),
        //    //    Email = Email
        //    //};

        //    //ComUserDTO ComUser = new ComUserDTO
        //    //{
        //    //    UserId = new string(userId.ToString()),
        //    //    ComId = null
        //    //};


        //    //var ChangeIdResult = await _client.PostAsJsonAsync("https://localhost:7008/api/v1/User/ChangeId", changeID);
        //    //var Company = await client.PostAsJsonAsync("https://localhost:7008/api/v1/ManageCompany/GetCompany", ComUser);
        //    TempData["CompanyName"] = ComResult2.Name;

        //    return RedirectToAction("MyTask", "Tasks", new { area = "Admin" });
        //}
        public IActionResult UserClaims()
        {
            ViewBag.accessToken = Request.Cookies["access_token"];

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                RegisterRequest registerRequest = new RegisterRequest();


                if (string.IsNullOrEmpty(model.userCompanyID))
                {
                    registerRequest.fullName = model.UserName;
                    //registerRequest.UserName = model.Email;
                    registerRequest.Password = model.Password;
                    registerRequest.Email = model.Email;
                    registerRequest.PhoneNumber = model.PhoneNumber;

                }
                else
                {
                    registerRequest.fullName = model.UserName;
                    //registerRequest.UserName = model.Email;
                    registerRequest.Password = model.Password;
                    registerRequest.Email = model.Email;
                    registerRequest.PhoneNumber = model.PhoneNumber;
                    registerRequest.userCompanyID = model.userCompanyID;
                    registerRequest.isCompanyUser = true;
                }

                var result = await _chitraclient.PostAsJsonAsync("Auth/register", registerRequest);

                if (!result.IsSuccessStatusCode)
                {
                    var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
                    ModelState.AddModelError(string.Empty, error.error);
                    return View(model);
                }
                var regResponse = await result.Content.ReadFromJsonAsync<RegResponse>();
                TempData["UserName"] = registerRequest.Email;


                if (Request.Cookies.ContainsKey("PostId"))
                {
                    var postId = Request.Cookies["PostId"];
                    RoleChangeDTO chnageRole = new RoleChangeDTO
                    {
                        userId = new string(regResponse.createduserId),
                        roleId = "001200b2-0b9c-403c-b42d-3ccc5dd84d00",
                        //comId = UserComId
                    };
                    var roleResult = await _chitraclient.PostAsJsonAsync("Role/AssignUserInRole", chnageRole);

                    if (!roleResult.IsSuccessStatusCode)
                    {
                        return View(roleResult);
                    }

                    ApplicantDTO applicant = new ApplicantDTO
                    {
                        ApplicantId = new string(regResponse.createduserId),
                        FirstName = model.Email,
                    };

                    bool res = _unitOfWork.applicantRepository.CreateApplicant(applicant);

                    await _chitraclient.PostAsync($"Auth/SendOtp?userName={model.Email}", null);

                    return RedirectToAction(nameof(ConfirmOTP));
                }



                if (!string.IsNullOrEmpty(model.userCompanyID))
                {
                    string? UserComId = (model.userCompanyID);
                    AUserDTO newUser = new AUserDTO
                    {
                        Id = new string(regResponse.createduserId),
                        UserName = model.UserName,
                        Email = model.Email,
                        //Role = Core.Enums.Role.SuperAdmin,
                        Phone = model.PhoneNumber,
                        ComId = UserComId
                    };





                    //var CreateUser = await _client.PostAsJsonAsync("User/Create", newUser);
                    //if (!CreateUser.IsSuccessStatusCode)
                    //{
                    //    return View(model);
                    //}
                }
                await _chitraclient.PostAsync($"Auth/SendOtp?userName={model.Email}", null);
                //Response.Cookies.Delete("PostId");

                return RedirectToAction(nameof(ConfirmOTP));
            }
            catch (Exception e)
            {
               
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
                throw new Exception(e.Message);
            }

            //}



            //LoginRequest loginmodel = new LoginRequest { UserName = model.UserName, Password = model.Password };
            //var autologin = await _chitraclient.PostAsJsonAsync("Auth/Login", loginmodel);
            //if ((int)autologin.StatusCode == 400)
            //{
            //    var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
            //    ModelState.AddModelError(string.Empty, error.error);
            //    return View(model);
            //}
            //if ((int)autologin.StatusCode == 321)
            //{
            //    return RedirectToAction(nameof(ConfirmOTP));
            //}
            //if ((int)autologin.StatusCode == 412)
            //{
            //    var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
            //    ModelState.AddModelError(string.Empty, error.error);
            //    return View(model);
            //}
            //if (!autologin.IsSuccessStatusCode)           
            //{
            //    var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
            //    ModelState.AddModelError(string.Empty, error.error);
            //    return View(model);
            //}
            //await result.Content.ReadFromJsonAsync<TokenResult>();


        }
        public IActionResult Register(string? ComId, string? postId)
        {
            //var response = _chitraclient.GetAsync("Company").Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    var data = response.Content.ReadAsStringAsync().Result;

            //    var companies = JsonConvert.DeserializeObject<List<CompanyDTO>>(data);
            //    ViewBag.Companies = companies;
            //}
            if (ComId != null)
            {
                ViewBag.ComId = ComId;
            }
            if (postId != null)
            {
                ViewBag.PostId = postId;
            }
            return View();
        }

        public async Task<IActionResult> EmailCheck(string email)
        {

            //var response = await _chitraclient.GetAsync("Auth/EmailIsExits", email);
            //var response = await _chitraclient.GetAsync("", email);
            var response = await _chitraclient.GetAsync($"Auth/EmailIsExits?email={email}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                bool emailExists = bool.Parse(responseContent);

                if (emailExists)
                {

                    return Content("EmailExists");
                }
                else
                {

                    return Content("EmailDoesNotExist");
                }
            }
            else
            {

                return Content("ApiError");
            }
        }

        public async Task<IActionResult> UserNameCheck(string name)
        {

            //var response = await _chitraclient.GetAsync("Auth/EmailIsExits", email);
            //var response = await _chitraclient.GetAsync("", email);
            var response = await _chitraclient.GetAsync($"Auth/UserNameIsExits?username={name}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                bool nameExists = bool.Parse(responseContent);

                if (nameExists)
                {

                    return Content("UserNameExists");
                }
                else
                {

                    return Content("UserNameDoesNotExist");
                }
            }
            else
            {

                return Content("ApiError");
            }
        }

        public IActionResult RegisterUser(string ComId)
        {

            if (ComId != null)
            {
                ViewBag.ComId = ComId;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            if (model.userCompanyID == null)
            {
                var error = new ErrorViewModel { error = "Please select company" };
                ModelState.AddModelError(string.Empty, error.error);
                return RedirectToAction(nameof(Register));
            }
            RegisterRequest registerRequest = new RegisterRequest
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                userCompanyID = model.userCompanyID
            };

            //CompanyDTO companyDTO = new CompanyDTO { CompanyName = model.Company ,Email = model.Email };

            //TokenResult resultToken = new TokenResult();

            // _chitraclient.DefaultRequestHeaders.Add("Chitra-API-Key", "ChitraManechitramanebagbonerchitranakintoatagtrarchitra");
            var result = await _chitraclient.PostAsJsonAsync("Auth/register", registerRequest);

            if (result.IsSuccessStatusCode)
            {
                //var resultToken = await result.Content.ReadAsStringAsync();
                LoginRequest loginmodel = new LoginRequest { UserName = model.UserName, Password = model.Password };
                var autologin = await _chitraclient.PostAsJsonAsync("Auth/Login", loginmodel);

                var data = result.Content.ReadAsStringAsync().Result;

                if (autologin.StatusCode != HttpStatusCode.BadRequest)
                {
                    var regResponse = JsonConvert.DeserializeObject<RegResponse>(data);
                    TempData["UserName"] = registerRequest.UserName;
                    AUserDTO newUser = new AUserDTO
                    {
                        Id = new string(regResponse.createduserId),
                        UserName = model.UserName,
                        Email = model.Email,
                        Role = Core.Enums.Role.User,
                        Phone = model.PhoneNumber,
                        ComId = new string(registerRequest.userCompanyID)
                    };
                    //companyDTO.User = newUser;

                    //if(model.Company == null)
                    //{
                    //    companyDTO.CompanyName = model.UserName + "'s Company";
                    //}

                    //HttpClient myclient = new HttpClient();

                    //var CreateUser = await _client.PostAsJsonAsync("User/Create", newUser);
                    //if (CreateUser.IsSuccessStatusCode)
                    //{
                    //    return RedirectToAction(nameof(ConfirmOTP));

                    //}

                    return RedirectToAction(nameof(ConfirmOTP));
                }
                return RedirectToAction(nameof(ConfirmOTP));
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
                ModelState.AddModelError(string.Empty, error.error);
                return View(model);
            }
        }

        public IActionResult ConfirmOTP()
        {
            OtpRequest data = new OtpRequest();

            data.UserName = TempData["UserName"] as string;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOTP(OtpRequest model)
        {
            var result = await _chitraclient.PostAsJsonAsync("Auth/EmailConfirmation", model);

            if (result.IsSuccessStatusCode)
            {
                var resultToken = await result.Content.ReadAsStringAsync();


                //var Token = await result.Content.ReadFromJsonAsync<TokenResult>();

                //await DecodeTokenAndCreateCookie(Token.access_token, HttpContext);
                //Response.Cookies.Append("access_token", Token.access_token, new CookieOptions
                //{
                //    HttpOnly = true
                //});
                //Response.Cookies.Append("refresh_token", Token.Refresh_token, new CookieOptions
                //{
                //    HttpOnly = true
                //});


                //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var Email = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

                //ChangeID changeID = new ChangeID {
                //    UserId = new string(userId.ToString()),
                //    Email = Email
                //};

                //client.DefaultRequestHeaders.Remove("Chitra-API-Key");

                //var ChangeIdResult = await client.PostAsJsonAsync("https://localhost:7008/api/v1/ChangeId", changeID);
                //if (ChangeIdResult.IsSuccessStatusCode)
                //{
                //    return RedirectToAction(nameof(Login));

                //}

                return RedirectToAction(nameof(Login));
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
                ModelState.AddModelError(string.Empty, error.error);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult CreateCompany(string userId, string ComId)
        {
            if (ComId != null)
            {
                CreateCompanyDTO viewModel = new CreateCompanyDTO
                {
                    Id = ComId,
                    UserId = new string(userId)
                };

                return View(viewModel);
            }
            else
            {
                // Handle invalid ComId value
                // For example, redirect to an error page or return a specific view
                return RedirectToAction();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDTO model)
        {
            var response = await _chitraclient.GetAsync("Company/" + model.Id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ComResult2 = JsonConvert.DeserializeObject<CompanyUpdateVM>(content);
                var ComResult = JsonConvert.DeserializeObject<CreateCompanyDTO>(content);

                if (ComResult2.Name == "Default Company")
                {
                    CompanyUpdateVM data = new CompanyUpdateVM()
                    {
                        Id = model.Id.ToString(),
                        //CreatedAt = ComResult2.CreatedAt,
                        //UpdatedAt = DateTime.Now,
                        IsDeleted = ComResult2.IsDeleted,
                        Name = model.Name,
                        IsActive = ComResult2.IsActive,
                        ReferbyPartnerId = ComResult2.ReferbyPartnerId,
                        ReferbyPartner = ComResult2.ReferbyPartner,
                        ParentId = ComResult2.ParentId,

                    };

                    var result = await _chitraclient.PutAsJsonAsync("Company/" + ComResult2.Id, data);
                }


                //model.Email = "test@gmail.com";
                //model.Id = ComResult.Id;
                //model.isActive = ComResult.isActive;

                ////if (result.IsSuccessStatusCode)
                ////{
                //    var companyResponse = await _client.PostAsJsonAsync("https://localhost:7008/api/v1/Company/CreateCom", model);
                var userResponse = await _chitraclient.GetAsync($"UserCompany/Userinfo?id={model.UserId}");
                var userContent = await userResponse.Content.ReadAsStringAsync();
                var userResult = JsonConvert.DeserializeObject<UserDTO>(userContent);

                CompanyDTO company = new CompanyDTO
                {
                    Id = new string(ComResult2.Id),
                    Email = userResult.Email,
                    Name = model.Name,
                    User = userResult
                };

               // var companyResponse = await _client.PostAsJsonAsync("Company/Create", company);
                //var companyResponse = await _client.PostAsJsonAsync("https://localhost:7008/api/v1/Company/Create", company);

                //if (companyResponse.IsSuccessStatusCode)
                //{
                //    TempData["CompanyName"] = model.Name;
                //}


                //}
                return RedirectToAction("MyTask", "Tasks", new { area = "Admin" });
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            TempData.Clear();
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("ComId");
            await HttpContext.SignOutAsync("Chitra");
            return RedirectToAction("Login", "Account", new { area = "" });
        }

        private async Task<ClaimsIdentity> DecodeTokenAndCreateCookie(string encodedToken, HttpContext httpContext)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = tokenHandler.ReadJwtToken(encodedToken);


            var identity = new ClaimsIdentity(token.Claims, "User");

            // Create an authentication ticket with the claims identity
            var principal = new ClaimsPrincipal(identity);
            var authenticationProperties = new AuthenticationProperties
            {

                ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.Claims.FirstOrDefault(x => x.Type == "exp")?.Value))
            };

            await httpContext.SignInAsync("Chitra", principal, authenticationProperties);

            return identity;

        }

        public IActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Forget(OtpVm email)
        {


            var apiUrl = $"Auth/SendOtp?userName={email.UserEmail}";

            HttpResponseMessage response = await _chitraclient.PostAsync(apiUrl, null); // No request content

            if (response.IsSuccessStatusCode)
            {
                TempData["UserEmail"] = email.UserEmail;
                return RedirectToAction("OtpConfirm");
            }

            return View();
        }


        public IActionResult OtpConfirm()
        {
            OtpRequest data = new OtpRequest();

            data.UserName = TempData["UserEmail"] as string;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> OtpConfirm(OtpRequest model)
        {

            var result = await _chitraclient.PostAsJsonAsync("Auth/VerifyOtp", model);

            if (result.IsSuccessStatusCode)
            {
                var resultToken = await result.Content.ReadAsStringAsync();
                TempData["UserEmail"] = model.UserName;
                return RedirectToAction(nameof(ResetPassword));
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
                ModelState.AddModelError(string.Empty, error.error);
                return View(model);
            }
        }

        public async Task<IActionResult> ResetPassword()
        {
            PasswordVM data = new PasswordVM();

            data.userName = TempData["UserEmail"] as string;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordVM model)
        {

            var result = await _chitraclient.PostAsJsonAsync("Auth/ResetPassword", model);

            if (result.IsSuccessStatusCode)
            {
                var resultToken = await result.Content.ReadAsStringAsync();
                return RedirectToAction(nameof(Login));
            }
            else
            {
                var error = await result.Content.ReadFromJsonAsync<ErrorViewModel>();
                ModelState.AddModelError(string.Empty, error.error);
                return View(model);
            }
        }

    }
}
