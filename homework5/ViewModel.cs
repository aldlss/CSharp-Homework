using System.Collections.Generic;

namespace homework5
{
    internal class ViewModel
    {
        private PenColor _selectPenColor;

        public CayleyTree CayleyTree { get; }

        public DelegationButton DelegationButton { get; }

        public List<PenColor> PenColors { get; }

        public PenColor SelectPenColor
        {
            get => _selectPenColor;
            set
            {
                _selectPenColor = value;
                CayleyTree.Pen = value.PenColorValue;
            }
        }

        public ViewModel()
        {
            CayleyTree = new CayleyTree();
            DelegationButton = new DelegationButton(
                (_) => CayleyTree.StartDrawCayleyTree(0, 0));
            PenColors = PenColor.CreatePenColorList();
            _selectPenColor = new PenColor();
        }
    }
}
