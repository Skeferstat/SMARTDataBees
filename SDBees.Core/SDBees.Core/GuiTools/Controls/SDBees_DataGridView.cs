﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SDBees.GuiTools.Controls
{
    [Description("DataGridView that Saves Column Order, Width and Visibility to user.config")]
    [ToolboxBitmap(typeof(System.Windows.Forms.DataGridView))]
    public class SDBees_DataGridView : DataGridView
    {
        public SDBees_DataGridView()
            : base()
        {
            this.KeyDown += this_KeyDown;
        }

        private void this_KeyDown(object sender, EventArgs e)
        {
            KeyEventArgs keyEventArgs = e as KeyEventArgs;

            if (keyEventArgs != null)
            {
                if (keyEventArgs.KeyCode == Keys.Escape)
                {
                    clearSelection();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            SaveColumnOrder();
            base.Dispose(disposing);
        }

        private void SetColumnOrder()
        {
            if (this.Columns.Count > 0)
            {
                if (!gfDataGridViewSetting.Default.ColumnOrder.ContainsKey(this.Name))
                    return;

                List<ColumnOrderItem> columnOrder = gfDataGridViewSetting.Default.ColumnOrder[this.Name];

                if (columnOrder != null)
                {
                    var sorted = columnOrder.OrderBy(i => i.DisplayIndex);
                    foreach (var item in sorted)
                    {
                        try
                        {
                            this.Columns[item.ColumnIndex].DisplayIndex = item.DisplayIndex;
                            this.Columns[item.ColumnIndex].Visible = item.Visible;
                            this.Columns[item.ColumnIndex].Width = item.Width;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        private void SaveColumnOrder()
        {
            if (this.Columns.Count > 0)
            {
                if (this.AllowUserToOrderColumns)
                {
                    List<ColumnOrderItem> columnOrder = new List<ColumnOrderItem>();
                    DataGridViewColumnCollection columns = this.Columns;
                    for (int i = 0; i < columns.Count; i++)
                    {
                        columnOrder.Add(new ColumnOrderItem
                        {
                            ColumnIndex = i,
                            DisplayIndex = columns[i].DisplayIndex,
                            Visible = columns[i].Visible,
                            Width = columns[i].Width
                        });
                    }

                    gfDataGridViewSetting.Default.ColumnOrder[this.Name] = columnOrder;
                    gfDataGridViewSetting.Default.Save();
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //SetColumnOrder();
            //LoadSortInformations();
        }

        bool m_dragColumnHeader = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                HitTestInfo hti = base.HitTest(e.X, e.Y);
                if ((AllowUserToOrderColumns) &&
                    (hti.Type == DataGridViewHitTestType.ColumnHeader) &&
                    ((e.Button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left))
                {
                    m_cacheCurrentColumnIndex = hti.ColumnIndex;

                    m_dragColumnHeader = true;
                }
            }
            catch (Exception ex)
            {
            }

            base.OnMouseDown(e);
        }

        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewColumn col = this.Columns[e.ColumnIndex];
                SaveSortInformations(col.Name, this.SortOrder);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            base.OnColumnHeaderMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            bool save = false;

            try
            {
                HitTestInfo hti = base.HitTest(e.X, e.Y);
                if ((AllowUserToOrderColumns) &&
                    (hti.Type == DataGridViewHitTestType.ColumnHeader) &&
                    ((e.Button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left) &&
                    m_dragColumnHeader)
                {
                    m_dragColumnHeader = false;
                    save = true;
                }
            }
            catch (Exception ex)
            {
            }

            base.OnMouseUp(e);

            if (save)
            {
                save = false;
                SaveColumnOrder();
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            //ApplySortAndOrder();
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            //ApplySortAndOrder();
        }

        private int m_cacheCurrentColumnIndex = 0;

        protected override void OnDataSourceChanged(EventArgs e)
        {
            m_cacheCurrentColumnIndex = 0;
            base.OnDataSourceChanged(e);
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            ApplySortAndOrder();
            base.OnDataBindingComplete(e);
            clearSelection();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            //ApplySortAndOrder();
        }

        private void clearSelection()
        {
            if (0 < this.Rows.Count)
            {
                if (m_cacheCurrentColumnIndex < this.Rows[0].Cells.Count)
                {
                    this.CurrentCell = this.Rows[0].Cells[m_cacheCurrentColumnIndex];
                }
            }

            this.ClearSelection();
        }

        private void ApplySortAndOrder()
        {
            if (DataSource != null)
            {
                foreach (DataGridViewColumn column in this.Columns)
                {
                    if (column.SortMode == DataGridViewColumnSortMode.NotSortable)
                    {
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                    }
                }

                this.SetColumnOrder();
                this.LoadSortInformations();
                //this.SaveSortInformations(this.SortColumn.Name, this.SortOrder);
            }
        }

        protected override void OnSorted(EventArgs e)
        {
            //if (!internalSorting)
            //{
            //    SaveSortInformations(this.SortedColumn.Name);
            //}

            base.OnSorted(e);
        }

        private void SaveSortInformations(string columnName, SortOrder sorder)
        {
            gfDataGridViewSetting.Default.ActiveSortColumn = columnName;
            gfDataGridViewSetting.Default.ActiveListSortOrder = sorder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            gfDataGridViewSetting.Default.Save();
            //SortColumn = columnName;
        }

        bool internalSorting = false;

        DataGridViewColumn m_sortColumn = null;
        public DataGridViewColumn MySortColumn
        {
            get { return m_sortColumn; }
            set { m_sortColumn = value; }
        }

        private void LoadSortInformations()
        {
            if (gfDataGridViewSetting.Default.ActiveSortColumn != null && !String.IsNullOrEmpty(gfDataGridViewSetting.Default.ActiveSortColumn))
            {
                if (this.Columns.Contains(gfDataGridViewSetting.Default.ActiveSortColumn))
                {
                    DataGridViewColumn col = this.Columns[gfDataGridViewSetting.Default.ActiveSortColumn];
                    if (col != null)
                    {
                        if (col.SortMode != DataGridViewColumnSortMode.NotSortable)
                        {
                            this.internalSorting = true;
                            MySortColumn = col;
                            SortOrder s = this.SortOrder;

                            ListSortDirection direction = gfDataGridViewSetting.Default.ActiveListSortOrder;

                            if (col.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                                direction = ListSortDirection.Ascending;
                            else if (col.HeaderCell.SortGlyphDirection == SortOrder.None)
                                direction = gfDataGridViewSetting.Default.ActiveListSortOrder;
                            else
                                direction = ListSortDirection.Descending;

                            if (internalSorting)
                                SaveSortInformations(col.Name, s);

                            Sort(col, direction);//gfDataGridViewSetting.Default.ActiveListSortOrder);

                            col.HeaderCell.SortGlyphDirection = gfDataGridViewSetting.Default.ActiveListSortOrder == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;


                            internalSorting = false;
                        }
                        else
                        {
                            MessageBox.Show("Can't sort using this column!", "Sorting", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            else
            {
                if (this.SortedColumn != null && this.SortOrder != SortOrder.None)
                {
                    SaveSortInformations(this.SortedColumn.Name, this.SortOrder);
                    LoadSortInformations();
                }
            }
        }
    }

    /// <summary>
    /// class for settings serialization
    /// </summary>
    internal sealed class gfDataGridViewSetting : ApplicationSettingsBase
    {
        private static gfDataGridViewSetting _defaultInstace = (gfDataGridViewSetting)ApplicationSettingsBase.Synchronized(new gfDataGridViewSetting());
        //---------------------------------------------------------------------
        public static gfDataGridViewSetting Default
        {
            get { return _defaultInstace; }
        }
        //---------------------------------------------------------------------
        // Because there can be more than one DGV in the user-application
        // a dictionary is used to save the settings for this DGV.
        // As key the name of the control is used.
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        [DefaultSettingValue("")]
        public Dictionary<string, List<ColumnOrderItem>> ColumnOrder
        {
            get { return this["ColumnOrder"] as Dictionary<string, List<ColumnOrderItem>>; }
            set { this["ColumnOrder"] = value; }
        }

        string m_activeSortColumn = "";
        public string ActiveSortColumn
        {
            get { return m_activeSortColumn; }
            set { m_activeSortColumn = value; }
        }

        ListSortDirection m_activeListSortDirection = ListSortDirection.Ascending;
        public ListSortDirection ActiveListSortOrder
        {
            get { return m_activeListSortDirection; }
            set { m_activeListSortDirection = value; }
        }
    }

    /// <summary>
    /// class for ColumnOrderItem for the columns in each datagridview
    /// </summary>
    [Serializable]
    public sealed class ColumnOrderItem
    {
        public int DisplayIndex { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public int ColumnIndex { get; set; }
    }
}