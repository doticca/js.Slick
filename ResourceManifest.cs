using Orchard.UI.Resources;

namespace js.Slick {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            // defaults at common highlight
            manifest.DefineScript("Slick")
                .SetDependencies("jQuery", "jQueryMigrate")
                .SetUrl("slick.min.js", "slick.js")                
                .SetVersion("1.4.1");

            manifest.DefineStyle("Slick_Core")
                .SetVersion("1.4.1")
                .SetUrl("slick.css", "slick.css");
            manifest.DefineStyle("Slick")
                .SetVersion("1.4.1")
                .SetDependencies("Slick_Core")
                .SetUrl("slick-theme.css", "slick-theme.css");
        }
    }
}
