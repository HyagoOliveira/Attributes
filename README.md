# Attributes

### What is this package for? ###

* This package contains a Unity Package for custom property attributes
* Current Version: 1.0.0

### What can I do with it? ###
* You can use two custom attributes:
	* **Required**: use it on strings, exposed and object reference types. If the value is null or empty, a error message will be displayed on Inspector bellow the given attribute.
	* **Tag**: use it only with a string fields. It'll replace the string field by a tag popup.
	
	![Showcase](https://bitbucket.org/nostgameteam/attributes/raw/2faa0df9f19bf41be056bf53ba039660b0b66035/Documentation/unity-package_attributes-showcase.jpg)

### How do I get set up? ###
* You can download this repo and place it inside your Unity project (the simplest way).
* Using **Unity Package Manager**:
	* Open the **manifest.json** file inside your Unity project's **Packages** folder;
	* For *versions 2018.3* or above, there are two options:
		* Using the **Package Registry Server**:
			* Add this line before *"dependencies"* node:
				* ```"scopedRegistries": [ { "name": "Action Code", "url": "http://34.83.179.179:4873/", "scopes": [ "com.actioncode" ] } ],```
			* The package **ActionCode-Attributes** will be avaliable for you to intall using the **Package Manager** windows.
		* By **Git URL** (you'll need a **Git client** installed on your machine):
			* Add this line inside *"dependencies"* node: 
				* ```"com.actioncode.attributes":"https://bitbucket.org/nostgameteam/attributes.git"**```

	* For *versions 2017.2* or below: 
		* Clone/download this repo in any folder on your machine;
		* Add this line inside *"dependencies"* node: 
			* ```"com.actioncode.attributes": "*[the-folder-path-you-download-it]*"```

### Who do I talk to? ###

* Repo owner and admin: **Hyago Oliveira** (hyagogow@gmail.com)