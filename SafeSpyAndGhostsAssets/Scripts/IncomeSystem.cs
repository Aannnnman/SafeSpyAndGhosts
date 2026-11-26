using UnityEngine;

public class IncomeSystem : MonoBehaviour
{
    [SerializeField] private Game _game;

    private void Update()
    {
        if (_game == null)
            return;

        double income = _game.TotalIncomePerSecond * Time.deltaTime;

        _game.AddCurrency(income);
    }
}
