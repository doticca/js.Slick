using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using js.Slick.Services;
using Orchard.Environment;
using Orchard.UI.Admin;


namespace js.Slick.Shapes
{
    public class SlickShapes : IShapeTableProvider
    {
        private readonly Work<WorkContext> _workContext;
        private readonly ISlickService _slickService;
        public SlickShapes(Work<WorkContext> workContext, ISlickService slickService)
        {
            _workContext = workContext;
            _slickService = slickService;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadLinks")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    if (!_slickService.GetAutoEnable()) return;
                    if (!_slickService.GetAutoEnableAdmin())
                    {
                        var request = _workContext.Value.HttpContext.Request;
                        if (AdminFilter.IsApplied(request.RequestContext))
                        {
                            return;
                        }
                    }


                        var resourceManager = _workContext.Value.Resolve<IResourceManager>();
                        var scripts = resourceManager.GetRequiredResources("stylesheet");

                        string includecss = "Slick";
                        // check to see if another module declared a script foundation. If so, we do nothing
                        var currentFoundation = scripts
                                .Where(l => l.Name == includecss)
                                .FirstOrDefault();

                        if (currentFoundation == null)
                        {
                            // temp untill we fix the logic
                            resourceManager.Require("stylesheet", includecss).AtHead();

                        }
                    
                });

            builder.Describe("HeadScripts")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    if (!_slickService.GetAutoEnable()) return;
                    if (!_slickService.GetAutoEnableAdmin())
                    {
                        var request = _workContext.Value.HttpContext.Request;
                        if (AdminFilter.IsApplied(request.RequestContext))
                        {
                            return;
                        }
                    }

                    var resourceManager = _workContext.Value.Resolve<IResourceManager>();
                    var scripts = resourceManager.GetRequiredResources("script");


                    string includejs = "Slick";
                    var currentHighlight = scripts
                            .Where(l => l.Name == includejs)
                            .FirstOrDefault();

                    if (currentHighlight == null)
                    {
                        resourceManager.Require("script", includejs).AtFoot();
                    }       
             
                });
        }
    }
}