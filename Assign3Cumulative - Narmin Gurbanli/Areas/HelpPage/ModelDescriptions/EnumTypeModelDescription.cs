using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assign3Cumulative___Narmin_Gurbanli.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}