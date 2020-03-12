import { Notify } from 'quasar'

Notify.setDefaults({
  position: 'top-right',
  timeout: 2500,
  progress: true,
  textColor: 'white',
  actions: [{ icon: 'close', color: 'white' }]
})