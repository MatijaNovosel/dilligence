function generateUserPictureSource(data) {
  if (data == null) {
    return require('../assets/default-user.jpg');
  }
  return 'data:image/png;base64,' + data;
}

function randInt(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

function randColor() {
  return `rgb(${this.randInt(0, 255)}, ${this.randInt(0, 255)}, ${this.randInt(0, 255)})`;
}

function acronym(s) {
  var words, acronym, nextWord;
  words = s.split(' ');
  acronym = "";
  var index = 0;
  while (index < words.length) {
    nextWord = words[index];
    acronym = acronym + nextWord.charAt(0);
    index++;
  }
  return acronym.slice(0, 2).toUpperCase();
}

function fileIcon(fileExtension) {
  switch (fileExtension) {
    case 'png':
    case 'jpg':
    case 'jpeg':
      return 'mdi-image';
    case 'sql':
      return 'mdi-database';
    case 'cpp':
      return 'mdi-language-cpp';
    case 'c':
      return 'mdi-language-c';
    case 'cs':
      return 'mdi-language-csharp';
    case 'css':
      return 'mdi-language-css3';
    case 'html':
      return 'mdi-language-html5';
    case 'java':
      return 'mdi-language-java';
    case 'js':
      return 'mdi-language-javascript';
    case 'py':
      return 'mdi-language-python';
    case 'pdf':
      return 'mdi-file-pdf';
    case 'php':
      return 'mdi-language-php';
    case 'ts':
      return 'mdi-language-typescript';
    case 'xlsx':
    case 'xslm':
    case 'xltx':
    case 'xltm':
      return 'mdi-file-excel';
    case 'pptx':
    case 'ppt':
      return 'mdi-file-powerpoint';
    case 'mp3':
    case 'avi':
    case 'ogg':
      return 'mdi-file-music';
    case 'zip':
    case 'rar':
    case '7zip':
      return 'mdi-zip-box-outline';
    case 'doc':
    case 'docx':
    case 'docm':
    case 'dotx':
    case 'docb':
      return 'mdi-file-word';
    case 'mp4':
    case 'flv':
      return 'mdi-video-vintage';
    case 'txt':
    case 'md':
      return 'mdi-text';
    default:
      return 'mdi-file';
  }
}

function download(contentType, base64Data, name) {
  var element = document.createElement('a');
  element.setAttribute('href', `data:${contentType};base64, ${base64Data}`);
  element.setAttribute('download', name);
  element.style.display = 'none';
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);
}

export {
  generateUserPictureSource,
  randInt,
  randColor,
  acronym,
  fileIcon,
  download
};