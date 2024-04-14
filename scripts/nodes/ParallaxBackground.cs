using Godot;
using Godot.Collections;

public partial class ParallaxBackground : Godot.ParallaxBackground
{
    private Array<Array<CompressedTexture2D>> _backgrounds = new()
    {
        new Array<CompressedTexture2D>
        {
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/sky.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/clouds_1.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/clouds_2.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/clouds_3.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/clouds_4.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/rocks_1.png"),
            GD.Load<CompressedTexture2D>("res://assets/sprites/placeholder_parallax/bg1/rocks_2.png")
        }
    };

    [Export(PropertyHint.Range, "0,0,1")] public int Level;
    [Export] public Vector2 OffSetBackground = new(0, 540);

    private TextureRect _exampleTextureRect;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _exampleTextureRect = GetNode<TextureRect>("Example");
        _exampleTextureRect.Hide();
        var backgrounds = _backgrounds[Level];
        var inc = 1f / backgrounds.Count;

        for (var i = 0; i < backgrounds.Count; i++)
        {
            var texture = backgrounds[i];


            var motionScale = i == 0 ? 1 : inc * i;
            var layer = new ParallaxLayer
            {
                MotionScale = new Vector2(motionScale, 1),
                MotionMirroring = new Vector2(texture.GetSize().X, 0),
            };
            var sprite = new Sprite2D
            {
                Texture = texture,
                Offset = OffSetBackground,
            };
            layer.AddChild(sprite);

            AddChild(layer);
        }
    }
}