using System.Net.Mail;
using System.Text;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace EmailDemo;
class program{
    static async Task Main(string[] args)
{
     var sender = new SmtpSender(() => new SmtpClient(host:"localhost")
     {
        EnableSsl = false,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Port = 25
        // DeliveryMethod = smtpDeliveryMethod.SpecifiedPickupDirectory,
        // PickupDirectoryLocation = @"c:\Demos"

     });
       StringBuilder template = new();
       template.AppendLine(value:"Dear @Model.FirstName,");
       template.AppendLine(value:"<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it.</p>");
       template.AppendLine(value:"-The Timco Team");
     Email.DefaultSender = sender;
     Email.DefaultRenderer = new RazorRenderer();
     var email = await Email
     .From(emailAddress:"tim@timco.com")
     .To(emailAddress:"test@test.com", name:"Sue") 
     .Subject(subject:"Thanks!")
     .UsingTemplate(template.ToString(), new{ FirstName = "Tim", ProductName = "Bacon-Wrapped Bacon"})
     //.Body(body:"Thanks for buying our product.")
     .SendAsync();
}
}  