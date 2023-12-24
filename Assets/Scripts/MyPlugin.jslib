mergeInto(LibraryManager.library, {
  GetDeviceType: function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
      return 1; // Mobile
    } else {
      return 0; // Desktop
    }
  }
});
