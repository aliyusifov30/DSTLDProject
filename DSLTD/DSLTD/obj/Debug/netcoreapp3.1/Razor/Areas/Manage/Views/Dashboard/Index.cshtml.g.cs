#pragma checksum "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7d6fcff24ba6ec56a604b363bf23b5ad72ddd743"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Manage_Views_Dashboard_Index), @"mvc.1.0.view", @"/Areas/Manage/Views/Dashboard/Index.cshtml")]
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
#nullable restore
#line 2 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\_ViewImports.cshtml"
using DSLTD.Areas.Manage.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\_ViewImports.cshtml"
using DSLTD.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d6fcff24ba6ec56a604b363bf23b5ad72ddd743", @"/Areas/Manage/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eba4e1bcdc47d4d2d04f1c0bb8ad23f46d48f39f", @"/Areas/Manage/Views/_ViewImports.cshtml")]
    public class Areas_Manage_Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DashboardViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\Dashboard\Index.cshtml"
  
    double allComment = Model.ProductComments.Count();
    double acceptComment = Model.ProductComments.Where(x => x.Status == true).Count();

    double percentComment = (acceptComment * 100 ) / allComment;
    string percentCommentStr = percentComment + "%";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                    <h1 class=""m-0"">Dashboard</h1>
                </div><!-- /.col -->
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                        <li class=""breadcrumb-item active"">Dashboard v1</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <!-- Small boxes (Stat box) -->
            <div class=""row"">
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
           ");
            WriteLiteral("         <div class=\"small-box bg-info\">\r\n                        <div class=\"inner\">\r\n                            <h3>\r\n                                ");
#nullable restore
#line 39 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\Dashboard\Index.cshtml"
                           Write(Model.NewOrders.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </h3>
                            <p>New Orders</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-bag""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>

                <div class=""col-md-3 col-sm-6 col-12"">
                    <div class=""info-box bg-danger"" style=""height: 142px;"">
                        <span class=""info-box-icon""><i class=""fas fa-comments""></i></span>

                        <div class=""info-box-content"">
                            <span class=""info-box-text"">Comments</span>
                            <span class=""info-box-number"">
                                ");
#nullable restore
#line 57 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\Dashboard\Index.cshtml"
                           Write(Model.ProductComments.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </span>\r\n                            <div class=\"progress\">\r\n                                <div class=\"progress-bar\"");
            BeginWriteAttribute("style", " style=\"", 2552, "\"", 2585, 2);
            WriteAttributeValue("", 2560, "width:", 2560, 6, true);
#nullable restore
#line 60 "C:\Users\SWIFT\Desktop\new\BackEnd\DSTLDBackEnd\DSLTD\DSLTD\Areas\Manage\Views\Dashboard\Index.cshtml"
WriteAttributeValue(" ", 2566, percentCommentStr, 2567, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"></div>
                            </div>
                            <span class=""progress-description"">
                                Accept comment
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <!-- /.info-box -->
                </div>
                <!-- ./col -->
            </div>
            <!-- /.row -->
            <!-- Main row -->
        </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DashboardViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591