js.Slick
=============

A common location for Slick.js and related script libraries for Orchard Project.

This module defines a script manifest for Slick Library with name "Slick".<br>
You can include Slick script and styles inside your Razor views using:<br>
Script.Require("Slick")<br>
Style.Require("Slick")<br>

Slick module will automatically insert your Slick.js script in every page.<br>
You can disable this bevavior and include slick on demand (using Script.Require("Slick) inside your theme/view) by unchecking Auto Enable at slick module settings.<br>