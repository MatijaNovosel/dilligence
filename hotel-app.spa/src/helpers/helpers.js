export const Helper = Object.freeze({
  randInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  },
  randColor() {
    var color = `rgb(${this.randInt(0, 255)}, ${this.randInt(0, 255)}, ${this.randInt(0, 255)})`;
    return color;
  },
  acronym(s) {
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
  },
});