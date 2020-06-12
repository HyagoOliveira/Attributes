# Attributes

### What is this package for? ###

* This package contains a Unity Package for custom property attributes
* Current Version: 1.1.0
* Unity version: 2018.3

### What can I do with it? ###
* You can use two custom attributes:
	* **Required**: use it on strings, exposed and object reference types. If the value is null or empty, a error message will be displayed on Inspector bellow the given attribute.
	* **Tag**: use it only with a string fields. It'll replace the string field by a tag popup.
	* ![Attribute Showcase](/Documentation~/unity-package_attributes-showcase.jpg)

### How do I get set up? ###
* Using the **Package Registry Server**:
	* Open the **manifest.json** file inside your Unity project's **Packages** folder;
	* Add this line before *"dependencies"* attribute:
		* ```"scopedRegistries": [ { "name": "Action Code", "url": "http://34.83.179.179:4873/", "scopes": [ "com.actioncode" ] } ],```
	* The package **ActionCode-Attributes** will be avaliable for you to intall using the **Package Manager** windows.
	
* By **Git URL** (you'll need a **Git client** installed on your machine):
	* Add this line inside *"dependencies"* attribute: 
		* ```"com.actioncode.attributes":"https://bitbucket.org/nostgameteam/attributes.git"```
		
* Using it as a local package: 
	* Clone/download this repo in any folder on your machine;
	* Add this line inside *"dependencies"* attribute: 
		* ```"com.actioncode.attributes": "[the-folder-path-you-download-it]"```

### Who do I talk to? ###

* Repo owner and admin: **Hyago Oliveira** (hyagogow@gmail.com)