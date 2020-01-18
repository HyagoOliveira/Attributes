# Attributes

### What is this package for? ###

* This package contains a Unity Package for custom property attributes
* Current Version: 1.0.0	

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