var OpenWindowPlugin = {
    openWindow: function(link)
    {
        var url = Pointer_stringify(link);
        document.onmouseup = function()
        {
            //window.open(url);
            var button = document.createElement("a");
            button.setAttribute("href", url);
            button.setAttribute("download", "Mining Matters The Mine Win" + ".png");
            button.style.display = "none";
            document.body.appendChild(button);
            button.click();
            document.body.removeChild(button);

            document.onmouseup = null;
        }
    }
};
mergeInto(LibraryManager.library, OpenWindowPlugin);
