# Android WebView in Xamarin
Android Xamarin program that utilizes android webview to test websites. It loads webpages using different useragents (edge, desktop, mobile). It begins with edge, then desktop, and finally mobile. This android app was made to test loading webpages consecutively with random delay times in between each load.

**Layout**
---------------
The UI of the program is based off of a multitabbed application. Where the bottom row is the navigation bar, the top is the application title, the middle is the webview form, and above the webview form is a textview form (the text that says "Welcome" in the image below). I have set up the program so that instead of using the navigation bar as different tabs, it'll be used as buttons that call for certain commands. The textview part will be used to determine what the program is doing (Ex: stopping a command or the progress of the command).

<p align="center">
<img src="https://user-images.githubusercontent.com/100814612/166318836-521e61be-af6c-4f78-ab56-19600efebf43.png" width="281" height="600"><img>
</p>

**Navigation Bar**
---------------
When the "Run" tab is pressed on, the program initiates. Run sends a command to the program to start loading webpages. I have set up the program so that it will load edge, firefox, and mobile webpages in that order. More specifically, it will load 4 edge webpages, 4 desktop pages, and finally 4 mobile pages. Currently, the program is set to load random google search pages. All of this can be edited in the source code itself.

<p align="center">
<img src="https://user-images.githubusercontent.com/100814612/166319876-3fa4ef66-ced5-4554-b0cb-abdd77c8a997.png" width="281" height="600"><img>
</p>

**Safety Measures and Cancelation**
---------------
If you press run while the method is already running, I have implemented safety measures to cancel the process instead of running again. Pressing mobile and stop will also stop the method. In addition to that, both mobile and stop have an additional feature. Mobile will load a mobile webpage while stop will load a desktop webpage.

**Random Webpages**
---------------
Let's say you're trying to test loading random webpages on a search engine like google search. The program has a random string generator that you can append to the end of a link. For example, if you want to search for "define hello" in google search, notice the link looks like
```
https://www.google.com/search?q=define+hello
```
To get a random search result, we can append the random string to:
```
https://www.google.com/search?q=
```

**Editing the Source Code**
---------------
To edit the methods and webpages, refer to the file "MainActivity.cs". To edit the UI refer to the file "activity_main.xml" located in the Resources/layout folder. To edit the navigation bar, refer to the file "navigation.xml" in the Resources/menu folder.
