using Books_Store_Management_App.ViewModels;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Helpers;
using QuestPDF.Fluent;

namespace Books_Store_Management_App.Helpers
{
    public class InvoiceDocument : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Assets\\logo.png");

        public InvoiceViewModel Model { get; }

        public InvoiceDocument(InvoiceViewModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column
                        .Item().Text($"Invoice #{Model.Order.ID}")
                        .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    column.Item().Text(text =>
                    {
                        text.Span("Order Date: ").SemiBold();
                        text.Span($"{Model.Order.Date:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Invoice Date: ").SemiBold();
                        var date = DateTime.Now;
                        text.Span($"{date:d}");
                    });
                });

                row.ConstantItem(175).Image(LogoImage);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent("From", Model.SellerAddress));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new AddressComponent("Bill To", Model.CustomerAddress));
                });

                column.Item().Element(ComposeTable);

                var currencyConverter = new DoubleToUsdCurrencyConverter();

                column.Item().PaddingRight(5).AlignRight().Row(row =>
                {
                    // Cột cho các nhãn (Text)
                    row.RelativeItem(1);
                    row.RelativeItem().AlignLeft().Text("SubTotal:").SemiBold();
                    //row.RelativeItem().AlignRight().Text($"{currencyConverter.Convert(Model.SubTotal, typeof(string), null, null)}").SemiBold();
                    row.RelativeItem().AlignRight().Text($"{currencyConverter.Convert(Model.SubTotal, typeof(string), null, null)}").SemiBold();
                });

                column.Item().PaddingRight(5).AlignRight().Row(row =>
                {
                    // Không Biết Có nên để Tax không
                    row.RelativeItem(1);
                    row.RelativeItem().AlignLeft().Text("Tax (0%):").SemiBold();
                    row.RelativeItem().AlignRight().Text($"{currencyConverter.Convert(Model.SubTotal * 0, typeof(string), null, null)}").SemiBold();
                });

                column.Item().PaddingRight(5).AlignRight().Row(row =>
                {
                    var converter = new DoubleToPercentageConverter();
                    var discount = converter.Convert(Model.Order.Discount, null, null, null);
                    row.RelativeItem(1);
                    row.RelativeItem().AlignLeft().Text($"Discount ({discount}):").SemiBold();
                    row.RelativeItem().AlignRight().Text($"{currencyConverter.Convert(Model.TotalDiscount, typeof(string), null, null)}").SemiBold();
                });

                column.Item().PaddingRight(5).AlignRight().Row(row =>
                {
                    row.RelativeItem(1);
                    row.RelativeItem().AlignLeft().Text("Grand total:").SemiBold();
                    row.RelativeItem().AlignRight().Text($"{currencyConverter.Convert(Model.Order.Price, typeof(string), null, null)}").SemiBold();
                });

                //if (!string.IsNullOrWhiteSpace(Model.Comments))
                //    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }

        void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("Product").Style(headerStyle);
                    header.Cell().AlignRight().Text("Unit price").Style(headerStyle);
                    header.Cell().AlignRight().Text("Quantity").Style(headerStyle);
                    header.Cell().AlignRight().Text("Total").Style(headerStyle);

                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });

                var currencyConverter = new DoubleToUsdCurrencyConverter();

                foreach (var item in Model.Order.OrderItems)
                {
                    var index = item.Book.Index;

                    table.Cell().Element(CellStyle).Text($"{index}");
                    table.Cell().Element(CellStyle).Text(item.Book.Title);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{currencyConverter.Convert(item.Book.Price, typeof(string), null, null)}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{currencyConverter.Convert(item.SubTotal, typeof(string), null, null)}");

                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }

        //void ComposeComments(IContainer container)
        //{
        //    container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        //    {
        //        column.Spacing(5);
        //        column.Item().Text("Comments").FontSize(14).SemiBold();
        //        column.Item().Text(Model.Comments);
        //    });
        //}
    }

    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private Address Address { get; }

        public AddressComponent(string title, Address address)
        {
            Title = title;
            Address = address;
        }

        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1);

                column.Item().Text(Address.CompanyName);
                column.Item().Text(Address.Street);
                column.Item().Text($"{Address.City}, {Address.State}");
                column.Item().Text(Address.Email);
                column.Item().Text(Address.Phone);
            });
        }
    }

}
