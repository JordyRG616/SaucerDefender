using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechCreator : MonoBehaviour
{
    [SerializeField] private TechPiece pieceModel;
    [Space]
    [SerializeField] private List<TechCreatorSlot> slots;

    private int filledSlots;
    private TechBoard board;


    private void Start()
    {
        board = FractaMaster.GetManager<TechBoard>();

        foreach (var slot in slots)
        {
            slot.OnSamplePlaced.Register(() => filledSlots++);
            slot.OnSampleCleared.Register(() => filledSlots--);
        }
    }

    public void Build()
    {
        if (filledSlots != slots.Count) return;

        var piece = Instantiate(pieceModel);
        piece.ReceiveSamples(slots);
        board.ReceivePiece(piece);

        slots.ForEach(x => x.Clear());
    }
}
