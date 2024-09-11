## MRT

### shader部分

两个pass，第一个pass负责将东西放到两个rt上，第二个pass负责将两个rt上的东西合并放到最后的效果上

### 脚本部分

1 定义出来RenderBuffer[]对应shader里的两个SV_TARGET   

2  Graphics.SetRenderTarget中的第一个参数告诉GPU渲染目标是这个buffer数值了！

3 两次 Graphics.Blit(source, destination, material, pass);

其中第一次pass填0， 第二次pass填1      





