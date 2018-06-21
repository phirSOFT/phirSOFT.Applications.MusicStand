using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace phirSOFT.Applications.MusicStand.Contract
{
    /// <summary>
    /// Helps to resolve the correct data template to display a song part.
    /// </summary>
    public interface ISongPartSelectorService
    {
        /// <summary>
        /// Gets the DataTemplateSelector
        /// </summary>
        /// <returns></returns>
        DataTemplateSelector GetTemplateSelector();

        /// <summary>
        /// Registres a Presenter for one ore more required types.
        /// </summary>
        /// <param name="presenterType">The type of the presenter</param>
        /// <param name="requiredTypes">The types required to use this presenter</param>
        void RegisterPresenter(Type presenterType, params Type[] requiredTypes);

        /// <summary>
        /// Registres a Presenter for one ore more required types.
        /// </summary>
        /// <param name="presenterType">The type of the presenter</param>
        /// <param name="requiredTypes">The types required to use this presenter</param>
        void RegisterPresenter(Type presenterType, IEnumerable<Type> requiredTypes);
    }
}
