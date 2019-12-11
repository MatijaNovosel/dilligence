<template>
  <div>
    
  </div>
</template>

<script>

  export default {
    props: [ 'keyCode', 'event' ],
    mounted() {
      window.addEventListener(this.event, this.emitEvent);  
    },
    destroyed() {
      window.removeEventListener(this.event, this.emitEvent);  
    },
    methods: {
      emitEvent() {
        if(event.which == this.keyCode) {
          this.$emit("pressed");
        }
      }
    }
  }
  
</script>