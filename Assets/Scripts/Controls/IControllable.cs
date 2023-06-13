public interface IControllable
{
    public void MoveControllable(float horizontalInput, float verticalInput);
    public void Jump(KeyState state);
    public void Attack();
    public void Block();
    public void Dash();
}
