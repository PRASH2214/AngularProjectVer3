#pragma checksum "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f4aa59ba31b960e581fcba423d1f73f0561da3d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Response_Index), @"mvc.1.0.view", @"/Views/Response/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f4aa59ba31b960e581fcba423d1f73f0561da3d7", @"/Views/Response/Index.cshtml")]
    public class Views_Response_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<html lang=\"en\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f4aa59ba31b960e581fcba423d1f73f0561da3d72706", async() => {
                WriteLiteral(@"
    <!-- Required meta tags -->
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
    <title>e-Cubix</title>
    <!-- plugins:css -->
    <link rel=""stylesheet"" href=""https://dev-econsultation.ecubix.com/assets/vendors/mdi/css/materialdesignicons.min.css"">
    <link rel=""stylesheet"" href=""https://dev-econsultation.ecubix.com/assets/vendors/css/vendor.bundle.base.css"">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <!-- endinject -->
    <!-- Layout styles -->
    <link rel=""stylesheet"" href=""https://dev-econsultation.ecubix.com/assets/css/style.css"">
    <!-- Layout custom styles -->
    <link rel=""stylesheet"" href=""https://dev-econsultation.ecubix.com/assets/css/custom.css"">
    <!-- End layout styles -->
    <link rel=""shortcut icon"" href=""https://dev-econsultation.ecubix.com/assets/images/favicon.ico"">
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f4aa59ba31b960e581fcba423d1f73f0561da3d74684", async() => {
                WriteLiteral(@"
    <div class=""container-scroller"">
        <!-- navbar -->
        <nav class=""navbar default-layout-navbar col-lg-12 col-12 p-0 fixed-top d-flex flex-row"">
            <div class=""text-center navbar-brand-wrapper d-flex align-items-center justify-content-center"">
                <a class=""navbar-brand brand-logo"" href=""index.html""><img src=""https://dev-econsultation.ecubix.com/assets/images/ecubix-logo.svg"" alt=""logo""></a>
                <a class=""navbar-brand brand-logo-mini"" href=""index.html""><img src=""https://dev-econsultation.ecubix.com/assets/images/ecubix-logo-mini.jpeg"" alt=""logo""></a>
            </div>
            <div class=""navbar-menu-wrapper d-flex align-items-stretch"">
                <ul class=""navbar-nav navbar-nav-right"">
");
                WriteLiteral(@"                    <li class=""nav-item nav-profile "">
                        <a class=""nav-link"" href=""https://dev-econsultation.ecubix.com/patient/login"">

                            <div class=""nav-profile-text "">
                                <p class=""mb-1 text-black font-weight-normal text-white"">Patient Login</p>
                            </div>
                        </a>

                    </li>
");
                WriteLiteral(@"                </ul>
                <button class=""navbar-toggler navbar-toggler-right d-lg-none align-self-center text-white "" type=""button"" data-toggle=""offcanvas"">
                    <span class=""mdi mdi-menu""></span>
                </button>
            </div>
        </nav>
        <!-- partial -->
        <div class=""container-fluid page-body-wrapper"">
            <!-- partial:partials/_sidebar.html -->
            <nav class=""sidebar sidebar-offcanvas d-md-none"" id=""sidebar"">
                <ul class=""nav"">

                    <li class=""nav-item"">
                        <a class=""nav-link"" href=""#"">
                            <span class=""menu-title"">Patient Registration</span>

                        </a>
                    </li>

                    <li class=""nav-item"">
                        <a class=""nav-link"" href=""#"">
                            <span class=""menu-title"">Patient Login</span>

                        </a>
                    </li>
             ");
                WriteLiteral(@"       <li class=""nav-item"">
                        <a class=""nav-link"" href=""#"">
                            <span class=""menu-title"">Doctor Login</span>

                        </a>
                    </li>








                </ul>
            </nav>
            <!-- partial -->
            <div class=""main-panel w-100"">
                <div class=""content-wrapper"">

                    <div class=""page-header bg-primary "">
                        <h3 class=""page-title text-center text-white py-3 w-100 radius-4"">Payment Response </h3>

                    </div>
                    <div class=""row dashboardcard"">
                        <div class=""col-md-12 grid-margin stretch-card "">
                            <div class=""card"">
                                <div class=""card-header header-sm"">

                                </div>
                                <div class=""card-body"">
                                    <div class="" row "">
                    ");
                WriteLiteral("                    <div class=\"col-md-6 form-group\">\r\n                                            <label>Appointment ID</label>\r\n                                            <label class=\"form-control\">");
#nullable restore
#line 115 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["orderId"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>

                                        </div>
                                        <div class=""col-md-6 form-group"">
                                            <label>Amount</label>
                                            <label class=""form-control"">");
#nullable restore
#line 120 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["orderAmount"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>
                                        <div class=""col-md-6 form-group"">
                                            <label>Payment Reference ID</label>
                                            <label class=""form-control"">");
#nullable restore
#line 124 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["referenceId"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>
                                        <div class=""col-md-6 form-group"">
                                            <label>Transaction Status</label>
                                            <label class=""form-control"">");
#nullable restore
#line 128 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["txStatus"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>

                                        <div class=""col-md-6 form-group"">
                                            <label>Payment Mode </label>
                                            <label class=""form-control"">");
#nullable restore
#line 133 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["paymentMode"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>

                                        <div class=""col-md-6 form-group"">
                                            <label>Message</label>
                                            <label class=""form-control"">");
#nullable restore
#line 138 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["txMsg"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>

                                        <div class=""col-md-6 form-group"">
                                            <label>Transaction Time</label>
                                            <label class=""form-control"">");
#nullable restore
#line 143 "E:\eConsultation\trunk\source\Cubix\Cubix\Views\Response\Index.cshtml"
                                                                   Write(ViewData["txTime"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</label>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <!-- Select Booking Schedule -->



                </div>
                <!-- content-wrapper ends -->
                <!-- partial:partials/_footer.html -->
                <footer class=""footer"">
                    <div class=""d-sm-flex justify-content-center justify-content-sm-between"">
                        <span class="" text-center text-sm-left d-block d-sm-inline-block""> © 2020  All rights reserved.</span>
                        <span class=""float-none float-sm-right d-block mt-1 mt-sm-0 text-center"">Powered by <img src=""https://dev-econsultation.ecubix.com/assets/images/ecubix-logo.svg"" width=""76px"" alt=""logo"" class=""ml-2""></span>
                    </div>


                </footer>
                <!-- partial -->
      ");
                WriteLiteral(@"      </div>
            <!-- main-panel ends -->
        </div>
        <!-- page-body-wrapper ends -->
    </div>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src=""https://dev-econsultation.ecubix.com/assets/vendors/js/vendor.bundle.base.js""></script>
    <!-- endinject -->
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src=""https://dev-econsultation.ecubix.com/assets/js/off-canvas.js""></script>
    <script src=""https://dev-econsultation.ecubix.com/assets/js/hoverable-collapse.js""></script>
    <script src=""https://dev-econsultation.ecubix.com/assets/js/misc.js""></script>
    <!-- endinject -->
    <!-- Custom js for this page
    <script src=""https://dev-econsultation.ecubix.com/assets/js/dashboard.js""></script>-->
    <!-- End custom js for this page -->

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
