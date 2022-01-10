# NHS Lateral Flow Checker Jan 2022

This is a simple piece of software which orders Lateral Flow Tests from the NHS Website.

It requires you to enter your NHS-registered username and password. It then opens a browser and completes the web form for you. If tests are unavailable, it waits five minutes and retries.

It runs on your local machine and does not send your email address or password anywhere, other than entering it into the NHS website. You can confirm this by checking the source (`MainWindow.cs`).

### Requirements

You must have:

1. An NHS account login. If you are not sure whether you have one, or what email address/password you used, you can check [here](https://www.nhsapp.service.nhs.uk/login).
2. An address registered to this account. This will automatically be the case if you have ordered tests online before.

The software is currently only available for Windows.

### Installation 

TODO: upload release and make release into a link.

1. Download the release.
2. Run `setup.exe`.
3. Click `Install`.

<img src="img/install-folder.png" style="width: 50%; height: 50%"/>

![Click Install](img/install.png?s=20)

The program will open automatically after being installed.

### Running after install

1. The program will be added to the Start Menu automatically be the installation.
2. Enter username and password and run the program.

![](img/start-menu.png?s=200)


![](img/main-window.png?s=100)

### Legal disclaimer

There is nothing in this code which infringes the gov.uk [terms and conditions](https://www.gov.uk/help/terms-conditions). This is not a denial-of-service attack: it makes, at most, one request every five minutes. 

This code does not introduce, or attempt to introduce, viruses, trojans, worms, logic bombs or any other material that’s malicious or technologically harmful. It does not attempt gain unauthorised access to GOV.UK, the server on which it’s stored or any server, computer or database connected to it.

The NHS prevents covid tests being ordered more than once every 24 hours and this program does not circumvent, or attempt to circumvent, this limit.