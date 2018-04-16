<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using System;

namespace UnityEngine.Rendering.PostProcessing
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class MinAttribute : Attribute
=======
namespace UnityEngine.PostProcessing
{
    public sealed class MinAttribute : PropertyAttribute
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
namespace UnityEngine.PostProcessing
{
    public sealed class MinAttribute : PropertyAttribute
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
namespace UnityEngine.PostProcessing
{
    public sealed class MinAttribute : PropertyAttribute
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
    {
        public readonly float min;

        public MinAttribute(float min)
        {
            this.min = min;
        }
    }
}
