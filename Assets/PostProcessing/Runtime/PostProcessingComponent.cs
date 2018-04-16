<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
    public abstract class PostProcessingComponentBase
    {
        public PostProcessingContext context;

        public virtual DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.None;
        }

        public abstract bool active { get; }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        public abstract PostProcessingModel GetModel();
    }

    public abstract class PostProcessingComponent<T> : PostProcessingComponentBase
        where T : PostProcessingModel
    {
        public T model { get; internal set; }

        public virtual void Init(PostProcessingContext pcontext, T pmodel)
        {
            context = pcontext;
            model = pmodel;
        }

        public override PostProcessingModel GetModel()
        {
            return model;
        }
    }

    public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public abstract CameraEvent GetCameraEvent();

        public abstract string GetName();

        public abstract void PopulateCommandBuffer(CommandBuffer cb);
    }

    public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public virtual void Prepare(Material material)
        {}
    }
}
=======
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
    public abstract class PostProcessingComponentBase
    {
        public PostProcessingContext context;

        public virtual DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.None;
        }

        public abstract bool active { get; }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        public abstract PostProcessingModel GetModel();
    }

    public abstract class PostProcessingComponent<T> : PostProcessingComponentBase
        where T : PostProcessingModel
    {
        public T model { get; internal set; }

        public virtual void Init(PostProcessingContext pcontext, T pmodel)
        {
            context = pcontext;
            model = pmodel;
        }

        public override PostProcessingModel GetModel()
        {
            return model;
        }
    }

    public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public abstract CameraEvent GetCameraEvent();

        public abstract string GetName();

        public abstract void PopulateCommandBuffer(CommandBuffer cb);
    }

    public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public virtual void Prepare(Material material)
        {}
    }
}
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
    public abstract class PostProcessingComponentBase
    {
        public PostProcessingContext context;

        public virtual DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.None;
        }

        public abstract bool active { get; }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        public abstract PostProcessingModel GetModel();
    }

    public abstract class PostProcessingComponent<T> : PostProcessingComponentBase
        where T : PostProcessingModel
    {
        public T model { get; internal set; }

        public virtual void Init(PostProcessingContext pcontext, T pmodel)
        {
            context = pcontext;
            model = pmodel;
        }

        public override PostProcessingModel GetModel()
        {
            return model;
        }
    }

    public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public abstract CameraEvent GetCameraEvent();

        public abstract string GetName();

        public abstract void PopulateCommandBuffer(CommandBuffer cb);
    }

    public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public virtual void Prepare(Material material)
        {}
    }
}
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
=======
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
    public abstract class PostProcessingComponentBase
    {
        public PostProcessingContext context;

        public virtual DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.None;
        }

        public abstract bool active { get; }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        public abstract PostProcessingModel GetModel();
    }

    public abstract class PostProcessingComponent<T> : PostProcessingComponentBase
        where T : PostProcessingModel
    {
        public T model { get; internal set; }

        public virtual void Init(PostProcessingContext pcontext, T pmodel)
        {
            context = pcontext;
            model = pmodel;
        }

        public override PostProcessingModel GetModel()
        {
            return model;
        }
    }

    public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public abstract CameraEvent GetCameraEvent();

        public abstract string GetName();

        public abstract void PopulateCommandBuffer(CommandBuffer cb);
    }

    public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public virtual void Prepare(Material material)
        {}
    }
}
>>>>>>> parent of 236db71... Merge branch 'master' of https://github.com/Militaratus/CMV2A_GameLab
