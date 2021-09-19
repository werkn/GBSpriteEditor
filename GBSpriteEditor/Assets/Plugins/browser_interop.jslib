mergeInto(LibraryManager.library, {

  /**
    NOTE: 
    Do not use modern Javascript here, var only, no let/const, template strings, ES6 etc...
    or this won't build.  Errors are cryptic in Unity but will say something along the lines of 
    "Build Engine failure, optimizing JS" if you use something not supported.
  */

  //Init browser state for things like palette colors, sprite data table
  //when launching the app
  InitBrowserState: function (palette0, palette1, palette2, palette3) {
    var palette0Selector = document.getElementById("palette0");
    var palette1Selector = document.getElementById("palette1");
    var palette2Selector = document.getElementById("palette2");
    var palette3Selector = document.getElementById("palette3");
    
    if (palette0Selector && palette1Selector && palette2Selector && palette3Selector) {
      palette0Selector.value = Pointer_stringify(palette0);
      palette1Selector.value = Pointer_stringify(palette1);
      palette2Selector.value = Pointer_stringify(palette2);
      palette3Selector.value = Pointer_stringify(palette3);
    } else {
      console.log("Unable to set palette as color selectors with id's palette0, palette1, palette2 and/or palette3 not found.");
    }
  },
  
  //Update sprite data table with correct sprite data when editing pixels
  UpdateSpriteDataTable: function (spriteAsString) {
    //due to lack of support for string functions we just pass the sprite along to
    //a method in the main broswer script (where we have access to ES6)
    syncSpriteData(Pointer_stringify(spriteAsString));
  },
  

});