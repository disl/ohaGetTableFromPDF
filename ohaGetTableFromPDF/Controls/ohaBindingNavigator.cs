namespace ohaERP_Library.DataGridView
{
    public partial class ohaBindingNavigator : BindingNavigator
    {
        bool m_ShowMoveControls = true;
        public bool ShowMoveControls
        {
            get { return m_ShowMoveControls; }
            set
            {
                m_ShowMoveControls = value;

                MoveFirstItem.Visible = m_ShowMoveControls;
                MovePreviousItem.Visible = m_ShowMoveControls;
                MoveNextItem.Visible = m_ShowMoveControls;
                MoveLastItem.Visible = m_ShowMoveControls;
                CountItem.Visible = m_ShowMoveControls;
                PositionItem.Visible = m_ShowMoveControls;

                if (!m_ShowMoveControls)
                {
                    foreach (ToolStripItem item in Items)
                    {
                        if (item.GetType() == typeof(ToolStripSeparator))
                            ((ToolStripSeparator)item).Visible = false;
                    }
                }
            }
        }

        bool m_ShowOnlyMoveControls = false;
        public bool ShowOnlyMoveControls
        {
            get { return m_ShowOnlyMoveControls; }
            set
            {
                m_ShowOnlyMoveControls = value;

                speichernToolStripButton.Visible = !m_ShowOnlyMoveControls;
                if (AddNewItem != null)
                    AddNewItem.Visible = !m_ShowOnlyMoveControls;
                if (DeleteItem != null)
                    DeleteItem.Visible = !m_ShowOnlyMoveControls;

                //MoveFirstItem.Visible = m_ShowMoveControls;
                //MovePreviousItem.Visible = m_ShowMoveControls;
                //MoveNextItem.Visible = m_ShowMoveControls;
                //MoveLastItem.Visible = m_ShowMoveControls;
                //CountItem.Visible = m_ShowMoveControls;
                //PositionItem.Visible = m_ShowMoveControls;

                if (!m_ShowMoveControls)
                {
                    foreach (ToolStripItem item in Items)
                    {
                        if (item.GetType() == typeof(ToolStripSeparator))
                            ((ToolStripSeparator)item).Visible = false;
                    }
                }
            }
        }

        public ToolStripButton speichernToolStripButton { get; set; }

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ohaBindingNavigator));

        public ohaBindingNavigator()
        {
            AddStandardItems();

            speichernToolStripButton = new ToolStripButton();
            resources.ApplyResources(speichernToolStripButton, "speichernToolStripButton");
            speichernToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            speichernToolStripButton.Name = "speichernToolStripButton";
            Items.Add(speichernToolStripButton);


            resources.ApplyResources(MoveFirstItem, "bindingNavigatorMoveFirstItem");
            resources.ApplyResources(MovePreviousItem, "bindingNavigatorMovePreviousItem");
            resources.ApplyResources(MoveNextItem, "bindingNavigatorMoveNextItem");
            resources.ApplyResources(MoveLastItem, "bindingNavigatorMoveLastItem");
            resources.ApplyResources(DeleteItem, "bindingNavigatorDeleteItem");
            resources.ApplyResources(AddNewItem, "bindingNavigatorAddNewItem");

            SetItemsSize35(MoveFirstItem);
            SetItemsSize35(MovePreviousItem);
            SetItemsSize35(MoveNextItem);
            SetItemsSize35(MoveLastItem);
            SetItemsSize35(DeleteItem);
            SetItemsSize35(AddNewItem);
            SetItemsSize35(speichernToolStripButton);


            MoveFirstItem.Visible = ShowMoveControls;
            MovePreviousItem.Visible = ShowMoveControls;
            MoveNextItem.Visible = ShowMoveControls;
            MoveLastItem.Visible = ShowMoveControls;
            CountItem.Visible = ShowMoveControls;


        }

        private void SetItemsSize35(ToolStripItem item)
        {
            item.AutoSize = false;
            item.Size = new System.Drawing.Size(35, 35);
        }
    }
}
