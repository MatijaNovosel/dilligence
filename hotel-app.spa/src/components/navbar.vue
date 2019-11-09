<template>
  <div>
    <v-navigation-drawer disable-route-watcher app clipped v-model="drawer" disable-resize-watcher>
      <v-list dense>
        <v-list-item to="/account">
          <v-list-item-avatar>
            <v-img src="../assets/matija.png"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>Matija Novosel</v-list-item-title>
            <v-list-item-subtitle>Information technologies</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-divider class="my-2" />
        <v-list-group v-for="item in drawerItems" :key="item.title" v-model="item.active" :prepend-icon="item.action" no-action>
          <template v-slot:activator>
            <v-list-item-content>
              <v-list-item-title v-text="item.title"></v-list-item-title>
            </v-list-item-content>
          </template>
          <v-list-item v-for="subItem in item.items" :key="subItem.title" router :to="subItem.link">
            <v-list-item-content>
              <v-list-item-title v-text="subItem.title"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-group>
      </v-list>
      <template v-slot:append>
        <div class="pa-3">
          <v-btn class="primary" block>
            Sign out 
            <v-icon class="ml-3"> mdi-logout </v-icon> 
          </v-btn>
        </div>
      </template>
    </v-navigation-drawer>
    <v-app-bar app clipped-left>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title class="mt-1">
        <v-avatar size="40" class="mr-5">
          <v-img src="../assets/tvz-logo.svg"></v-img>
        </v-avatar>
        <span class="title">Moj TVZ</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-badge class="mt-3 mb-1" color="cyan" left overlap>
          <v-btn icon color="grey" v-on="on">
            <v-icon large>mdi-bell</v-icon>
          </v-btn>
          <template v-slot:badge>
            <span>{{ notifications.length }}</span>
          </template>
        </v-badge>
        </template>
        <v-list>
          <v-list-item v-for="(item, i) in notifications" :key="i" router to="/notification">
            <v-list-item-title>{{ item.title }}</v-list-item-title>
            <v-list-item-action>
              <v-icon color="blue">mdi-information</v-icon>
            </v-list-item-action>
          </v-list-item>
        </v-list>
      </v-menu>      
    </v-app-bar>
  </div>
</template>

<script>

export default { 
  data() {
    return { 
      drawer: false,
      notifications: [
        { title: 'Baze podataka - nova vijest' },
        { title: 'Baze podataka - novi privitak' },
        { title: 'Nova obavijest studentske referade' }
      ],
      routeItems: [
        { title: 'Home', icon: 'mdi-home', route: '/' },
        { title: 'Music', icon: 'mdi-music', route: '/music' }
      ],
      drawerItems: [{
        action: "mdi-home",
        title: "General",
        items: [
          { title: 'Home', link: "/" },
          { title: 'Student notifications', link: "/studentNotifications" },
          { title: 'Electronic services', link: "/electronicServices" },
          { title: 'Literature search', link: "/literatureSearch" },
          { title: 'Quality', link: "/quality" },
          { title: 'Employee list', link: "/employeeList" },
          { title: 'Webmail', link: "/webmail" }
        ]
      }, {
        action: "mdi-pencil",
        title: "Personal",
        items: [
          { title: 'Deadlines', link: "/deadlines" },
          { title: 'News', link: "/news" },
          { title: 'Subjects', link: "/subjects" },
          { title: 'Change personal info', link: "/personalInfo" },
          { title: 'Daily spendings', link: "/calendar" },
          { title: 'My data', link: "/myData" }
        ]
      }, {
        action: "mdi-gesture-tap",
        title: "Actions",
        items: [
          { title: 'Choose a mentor', link: "/mentor" },
          { title: 'Scholarship calculator', link: "/scholarship" },
          { title: 'Make a request', link: "/request" }
        ]
      }, {
        action: "mdi-school",
        title: "Schooling",
        items: [
          { title: 'About', link: "/about" },
          { title: 'Goals', link: "/goals" },
          { title: 'Student council', link: "/studentCouncilAnnouncements" },
          { title: 'Calendar', link: "/calendar" }
        ]
      }]
    }
  }
};

</script>