function randInt(min, max) { 
  return Math.floor(Math.random() * (max - min + 1)) + min; 
}

function randColor(){ 
   return `rgb(${this.randInt(0, 255)}, ${this.randInt(0, 255)}, ${this.randInt(0, 255)})`;
}

function acronym(s) {
  var words, acronym, nextWord;
  words = s.split(' ');
  acronym = "";
  var index = 0;
  while(index < words.length) {
    nextWord = words[index];
    acronym = acronym + nextWord.charAt(0);
    index++;
  }
  return acronym.slice(0, 2).toUpperCase();
}

function fileIcon(fileExtension) {
  switch(fileExtension) {
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
    default:
      return 'mdi-file';
  }
}

export { randInt, randColor, acronym, fileIcon };