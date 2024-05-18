using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;

public class DepthNormalsFeature : ScriptableRendererFeature
{
    [SerializeField] public bool NormalTexture = false; // ���ر�SSAO��SSAOʹ��Depth Onlyʱ��������ѡ����Ⱦ����ͼ
    DepthNormalsPass mDepthNormalsPass;
    public override void Create()
    {
        mDepthNormalsPass = new DepthNormalsPass();
    }

    // ��Ϊÿ�����������һ����Ⱦ��ʱ�����ô˷���
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // �����Ҫ��Ⱦ���ߣ������
        if (NormalTexture)
        {
            renderer.EnqueuePass(mDepthNormalsPass);
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }

    // ��Ⱦ����Pass
    private class DepthNormalsPass : ScriptableRenderPass
    {
        // �����ʼ��
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // ��������ΪNormal����Unity RP���DepthNormalPrepass Pass
            ConfigureInput(ScriptableRenderPassInput.Normal);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // ʲô������������ֻ��Ҫ�������ʼ��ʱ����DepthNormals����
        }
    }
}