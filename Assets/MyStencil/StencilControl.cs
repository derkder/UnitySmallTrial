using UnityEngine;

public class StencilControl : MonoBehaviour
{
    public Material transparentMaterial; // 半透明物体的材质
    private bool stencilEnabled = false;

    void Update()
    {
        // 按下 "O" 键时开启遮挡
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableStencil();
        }

        // 按下 "C" 键时关闭遮挡
        if (Input.GetKeyDown(KeyCode.X))
        {
            DisableStencil();
        }
    }

    void EnableStencil()
    {
        // 设置模板缓冲区相关参数，启用模板测试
        transparentMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.NotEqual);
        stencilEnabled = true;
    }

    void DisableStencil()
    {
        // 禁用模板测试，让物体按正常的透明规则渲染
        transparentMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Disabled);
        stencilEnabled = false;
    }
}
