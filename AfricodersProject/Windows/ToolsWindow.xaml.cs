using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using IronPdf;
using System.Collections.Generic;
using System;
using System.Windows.Documents;
using System.Threading.Tasks;

namespace AfricodersProject
{
    /// <summary>
    /// Interaction logic for ToolsWindow.xaml
    /// </summary>
    public partial class ToolsWindow : Window
    {
        

        HtmlToPdf renderer;

        string headerText;
        string footerText;

        public ToolsWindow()
        {
            InitializeComponent();
        }

        private string savePath;
        private string htmlFilePath = string.Empty;
        
        private void topDocker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void GetHTMLConvertedToPDF()
        {
            PdfDocument pdfDocument = null;
            if (theBox.IsEnabled == true)
            {
                TextRange textRange = new TextRange(theBox.Document.ContentStart, theBox.Document.ContentEnd);
                pdfDocument = renderer.RenderHtmlAsPdf(textRange.Text);
            }
            else
            {
                //Use file instead
                pdfDocument = renderer.RenderHTMLFileAsPdf(htmlFilePath);

            }

            try
            {
                 string orientation = orientationComboBox.SelectedItem.ToString();

                    string paperSize = PaperSizeComboBox.SelectedItem.ToString();
                    renderer.PrintOptions.PaperOrientation = (PdfPrintOptions.PdfPaperOrientation)Enum.Parse(typeof(PdfPrintOptions.PdfPaperOrientation), orientation);

                    renderer.PrintOptions.PaperSize = (PdfPrintOptions.PdfPaperSize)Enum.Parse(typeof(PdfPrintOptions.PdfPaperSize), paperSize);

                    headerText = HeaderTextBlock.Text ?? string.Empty;

                    footerText = FooterTextBox.Text ?? string.Empty;


                    renderer.PrintOptions.Header = new SimpleHeaderFooter()
                    {
                        CenterText = headerText,
                        FontFamily = "Century Gothic",
                        FontSize = 15
                    };
                    renderer.PrintOptions.Footer = new SimpleHeaderFooter()
                    {
                        CenterText = footerText,
                        FontFamily = "Century Gothic",
                        FontSize = 15
                    };

                    string toSavePath = null;

                    if (savePath != null)
                    {
                        toSavePath = savePath + @"\" + Environment.UserDomainName + "File.pdf";

                        pdfDocument.SaveAs(toSavePath);
                    }
                    else
                    {
                        MessageBox.Show("Enter a path to save the PDF file");
                    }

            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString(), "Something not nice occured.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            GetHTMLConvertedToPDF();
             //await Task.Factory.StartNew(()=>GetHTMLConvertedToPDF(),System.Threading.CancellationToken.None, TaskCreationOptions.RunContinuationsAsynchronously,TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the html file path
            var fDialog = new OpenFileDialog();

            //Apply filter for html file only

            fDialog.Filter = "HTML File (*.html)|*.html";

            bool? showDialog = fDialog.ShowDialog();

            if (showDialog==true)
            {
                htmlFilePath = fDialog.FileName;

                SelectedFileText.Text = htmlFilePath;
                
            }

            theBox.IsEnabled = false;
        }

        private void theToolsWin_Loaded(object sender, RoutedEventArgs e)
        {

            theBox.Document.Blocks.Clear();

            theBox.AppendText("Paste your html content here. You can also select an HTML from your device. Note that this text area will become uninteractible if the latter option" +
                "is selected.");
            renderer = new HtmlToPdf();
            //set the default options
            renderer.PrintOptions.DPI = 300;

            renderer.PrintOptions.CreatePdfFormsFromHtml = true;

            renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;

            renderer.PrintOptions.MarginTop = 20;
            renderer.PrintOptions.MarginBottom = 20;

            orientationComboBox.ItemsSource = Enum.GetValues(typeof(PdfPrintOptions.PdfPaperOrientation));
            orientationComboBox.SelectedIndex = 0;
            PaperSizeComboBox.ItemsSource = Enum.GetValues(typeof(PdfPrintOptions.PdfPaperSize));
            PaperSizeComboBox.SelectedIndex = 0;

        }

        private void selectSavePathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult shownFolder = folderBrowserDialog.ShowDialog();

            if (shownFolder==System.Windows.Forms.DialogResult.OK)
            {
                savePath = folderBrowserDialog.SelectedPath;
            }

            SelectedSavePath.Text = savePath;
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
