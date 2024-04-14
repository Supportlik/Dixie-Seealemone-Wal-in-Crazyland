using Godot;

public partial class DialogManager : Node {

    private PackedScene _dialogContainerScene;
    private CanvasLayer _dialogContainer;
    private Label dialogText;

    public override void _Ready()
    {
        _dialogContainerScene = GD.Load<PackedScene>("res://scenes/nodes/dialog_container.tscn");
        _dialogContainer = _dialogContainerScene.Instantiate<CanvasLayer>();
        dialogText = _dialogContainer.GetNode<Label>("VBoxContainer/MarginContainer/Label");
        AddChild(_dialogContainer);
        dialogText.Visible = false;
    }

    public void ShowDialog(string text){
        dialogText.Text = text;
        dialogText.Visible = true;
    }

    public void HideDialog(){
        dialogText.Visible = false;
        dialogText.Text = "";
    }
}