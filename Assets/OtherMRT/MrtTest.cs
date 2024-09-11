using UnityEngine;
using static UnityEditor.ShaderData;

[ExecuteInEditMode]
public class MrtTest : MonoBehaviour
{
    [SerializeField] Shader _shader;

    Material _material;
    RenderBuffer[] _mrt;

    void OnEnable()
    {
        var shader = Shader.Find("Hidden/MrtTest");
        _material = new Material(shader);
        _material.hideFlags = HideFlags.DontSave;
        _mrt = new RenderBuffer[2];
    }

    void OnDisable()
    {
        DestroyImmediate(_material);
        _material = null;
        _mrt = null;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var rt1 = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default);
        var rt2 = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.DefaultHDR);

        _mrt[0] = rt1.colorBuffer;
        _mrt[1] = rt2.colorBuffer;

        // Blit with a MRT.
        Graphics.SetRenderTarget(_mrt, rt1.depthBuffer); // 接下来我要开始渲染了，我需要你把画画的地方设置为这两个目标,第一个参数告诉GPU颜色目标，第二个参数告诉gpu深度目标
        // 用 _material 中的 第一个 Pass（编号为 0）进行渲染。源纹理为空，这意味着没有指定输入的纹理。通常这表示默认将使用当前屏幕或渲染缓冲区作为源纹理。
        // destination没定义：处理后的结果会直接绘制回默认的目标（通常是屏幕或者当前的渲染缓冲区）[Graphics.Blit(source, destination, material, pass);]
        Graphics.Blit(null, _material, 0); 

        // Combine them and output to the destination.
        _material.SetTexture("_SecondTex", rt1);
        _material.SetTexture("_ThirdTex", rt2);
        Graphics.Blit(source, destination, _material, 1);

        RenderTexture.ReleaseTemporary(rt1);
        RenderTexture.ReleaseTemporary(rt2);
    }

    void OnGUI()
    {
        var text = "Supported MRT count: ";
        text += SystemInfo.supportedRenderTargetCount;
        GUI.Label(new Rect(0, 0, 200, 200), text);
    }
}
