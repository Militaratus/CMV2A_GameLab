<<<<<<< HEAD
<<<<<<< HEAD
using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class DitheringModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            public static Settings defaultSettings
            {
                get { return new Settings(); }
            }
        }

        [SerializeField]
        Settings m_Settings = Settings.defaultSettings;
        public Settings settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        public override void Reset()
        {
            m_Settings = Settings.defaultSettings;
        }
    }
}
=======
using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class DitheringModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            public static Settings defaultSettings
            {
                get { return new Settings(); }
            }
        }

        [SerializeField]
        Settings m_Settings = Settings.defaultSettings;
        public Settings settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        public override void Reset()
        {
            m_Settings = Settings.defaultSettings;
        }
    }
}
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class DitheringModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            public static Settings defaultSettings
            {
                get { return new Settings(); }
            }
        }

        [SerializeField]
        Settings m_Settings = Settings.defaultSettings;
        public Settings settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        public override void Reset()
        {
            m_Settings = Settings.defaultSettings;
        }
    }
}
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
