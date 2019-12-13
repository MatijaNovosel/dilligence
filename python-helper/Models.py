class Kolegij:
  def __init__(self, Ime, ECTS, KraticaSmjera, URL):
    self.Ime = Ime
    self.ECTS = ECTS
    self.KraticaSmjera = KraticaSmjera
    self.URL = URL
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, ECTS: {self.ECTS}, KraticaSmjera: {self.KraticaSmjera}, URL: {self.URL}"
  def __eq__(self, other):
      return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other):
      return not self.__eq__(other)

class KolegijURL:
  def __init__(self, URL, KraticaSmjera):
    self.KraticaSmjera = KraticaSmjera
    self.URL = URL
  def __str__(self) -> str:
    return f"KraticaSmjera: {self.KraticaSmjera}, URL: {self.URL}"
  def __eq__(self, other):
      return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other):
      return not self.__eq__(other)

class Student:
  def __init__(self, Ime, Prezime, JMBAG, Email, KraticaSmjera):
    self.Ime = Ime
    self.Prezime = Prezime
    self.JMBAG = JMBAG
    self.Email = Email
    self.KraticaSmjera = KraticaSmjera
  def __str__(self) -> str:
    return f"Ime: {self.Ime}, Prezime: {self.Prezime}, JMBAG: {self.JMBAG}, KraticaSmjera: {self.KraticaSmjera}, Email: {self.Email}"
  def __eq__(self, other):
      return (isinstance(other, self.__class__) and self.__dict__ == other.__dict__)
  def __ne__(self, other):
      return not self.__eq__(other)