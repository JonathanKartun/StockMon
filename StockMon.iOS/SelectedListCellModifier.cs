using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
public class CustomViewCellRenderer : ViewCellRenderer
{
    public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
    {
        var appResources = Xamarin.Forms.Application.Current.Resources;
        var selectionColour = appResources["selectedViewCellColour"];
        if (selectionColour == null) selectionColour = UIColor.Clear; else
            selectionColour = ((Color)selectionColour).ToUIColor();

       var cell = base.GetCell(item, reusableCell, tv);
        cell.SelectedBackgroundView = new UIView { BackgroundColor = (UIColor)selectionColour };
        //cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        return cell;
    }
}
