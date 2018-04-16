<<<<<<< HEAD
<<<<<<< HEAD
using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class UserLutModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            [Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
            public Texture2D lut;

            [Range(0f, 1f), Tooltip("Blending factor.")]
            public float contribution;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        lut = null,
                        contribution = 1f
                    };
                }
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
    public class UserLutModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            [Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
            public Texture2D lut;

            [Range(0f, 1f), Tooltip("Blending factor.")]
            public float contribution;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        lut = null,
                        contribution = 1f
                    };
                }
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
    public class UserLutModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            [Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
            public Texture2D lut;

            [Range(0f, 1f), Tooltip("Blending factor.")]
            public float contribution;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        lut = null,
                        contribution = 1f
                    };
                }
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
