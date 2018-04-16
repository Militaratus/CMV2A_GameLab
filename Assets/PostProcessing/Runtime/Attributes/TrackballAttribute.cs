<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using System;

namespace UnityEngine.Rendering.PostProcessing
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class TrackballAttribute : Attribute
    {
        public enum Mode
        {
            None,
            Lift,
            Gamma,
            Gain
        }

        public readonly Mode mode;

        public TrackballAttribute(Mode mode)
        {
            this.mode = mode;
=======
=======
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
namespace UnityEngine.PostProcessing
{
    public sealed class TrackballAttribute : PropertyAttribute
    {
        public readonly string method;

        public TrackballAttribute(string method)
        {
            this.method = method;
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
        }
    }
}
